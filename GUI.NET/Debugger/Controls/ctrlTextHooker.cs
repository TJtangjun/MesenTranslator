using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Mesen.GUI.Config;
using Mesen.GUI.Controls;
using Mesen.GUI.Forms;
using System.Collections.Concurrent;
using System.Drawing.Imaging;
using Mesen.GUI;

namespace Mesen.GUI.Debugger.Controls
{
	public partial class ctrlTextHooker : BaseControl
	{
		private byte[][] _nametablePixelData = new byte[4][];
		private byte[][] _tileData = new byte[4][];
		private byte[][] _attributeData = new byte[4][];
		private byte[] _tmpTileData = new byte[16];
		private byte[] _ppuMemory = new byte[0x4000];

		private Bitmap _nametableImage = new Bitmap(512, 480, PixelFormat.Format32bppPArgb);
		private Bitmap _outputImage = new Bitmap(512, 480, PixelFormat.Format32bppPArgb);
		private int _xScroll = 0;
		private int _yScroll = 0;
		private DebugState _state = new DebugState();
		private ConcurrentDictionary<string, string> _charMappings;
		private string _prevText;
		
		// 翻译相关字段
		private Timer _translateTimer;
		private string _lastTranslatedText = "";
		private bool _isTranslating = false;
		
		// 倒计时相关字段
		private DateTime _lastTranslateTime;
		private const int TranslateIntervalSeconds = 5;

		public ctrlTextHooker()
		{
			InitializeComponent();
			
			// 初始化翻译定时器
			_translateTimer = new Timer();
			_translateTimer.Interval = 1000; // 1秒间隔，用于更新倒计时显示
			_translateTimer.Tick += TranslateTimer_Tick;
			
			// 初始化倒计时时间
			_lastTranslateTime = DateTime.Now.AddSeconds(-TranslateIntervalSeconds);
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			if(!IsDesignMode) {
				DebugInfo debugInfo = ConfigManager.Config.DebugInfo;
				chkIgnoreMirroredNametables.Checked = debugInfo.TextHookerIgnoreMirroredNametables;
				chkUseScrollOffsets.Checked = debugInfo.TextHookerAdjustViewportScrolling;
				chkAutoCopyToClipboard.Checked = debugInfo.TextHookerAutoCopyToClipboard;
			}
		}

		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);

			if(!IsDesignMode) {
				DebugInfo debugInfo = ConfigManager.Config.DebugInfo;
				debugInfo.TextHookerIgnoreMirroredNametables = chkIgnoreMirroredNametables.Checked;
				debugInfo.TextHookerAdjustViewportScrolling = chkUseScrollOffsets.Checked;
				debugInfo.TextHookerAutoCopyToClipboard = chkAutoCopyToClipboard.Checked;
				
				// 停止并释放翻译定时器
				if(_translateTimer != null) {
					_translateTimer.Stop();
					_translateTimer.Dispose();
					_translateTimer = null;
				}
			}
		}

		public void GetData()
		{
			InteropEmu.DebugGetPpuScroll(out _xScroll, out _yScroll);
			InteropEmu.DebugGetState(ref _state);

			for(int i = 0; i < 4; i++) {
				InteropEmu.DebugGetNametable(i, NametableDisplayMode.Normal, out _nametablePixelData[i], out _tileData[i], out _attributeData[i]);
			}

			_ppuMemory = InteropEmu.DebugGetMemoryState(DebugMemoryType.PpuMemory);
			InteropEmu.DebugGetPpuScroll(out _xScroll, out _yScroll);
			_xScroll &= 0xFFF8;
			_yScroll &= 0xFFF8;
		}

		private string GetCharacter(int nt, int y, int x)
		{
			int outNt, outY, outX;
			GetIndexes(nt, y, x, out outNt, out outY, out outX);
			if(IgnoreTile(outNt)) {
				return " ";
			}

			string key = GetTileKey(outNt, (outY << 5) + outX);
			return GetMappedCharacter(key);
		}

		public void RefreshViewer()
		{
			using(Graphics gNametable = Graphics.FromImage(_nametableImage)) {
				for(int i = 0; i < 4; i++) {
					GCHandle handle = GCHandle.Alloc(_nametablePixelData[i], GCHandleType.Pinned);
					Bitmap source = new Bitmap(256, 240, 4*256, PixelFormat.Format32bppPArgb, handle.AddrOfPinnedObject());
					try {
						gNametable.DrawImage(source, new Rectangle(i % 2 == 0 ? 0 : 256, i <= 1 ? 0 : 240, 256, 240), new Rectangle(0, 0, 256, 240), GraphicsUnit.Pixel);
					} finally {
						handle.Free();
					}
				}
			}

			using(Graphics g = Graphics.FromImage(_outputImage)) {
				if(chkUseScrollOffsets.Checked) {
					g.DrawImage(_nametableImage, -_xScroll, -_yScroll);
					g.DrawImage(_nametableImage, -_xScroll + 512, -_yScroll + 480);
					g.DrawImage(_nametableImage, -_xScroll + 512, -_yScroll);
					g.DrawImage(_nametableImage, -_xScroll, -_yScroll + 480);
				} else {
					g.DrawImage(_nametableImage, 0, 0);
				}
			}
			picNametable.Image = _outputImage;

			StringBuilder output = new StringBuilder();
			DakutenType[] previousLineDakutenType = new DakutenType[32];
			for(int nt = 0; nt < 4; nt++) {
				for(int y = 0; y < 30; y++) {
					StringBuilder lineOutput = new StringBuilder();
					for(int x = 0; x < 32; x++) {
						string value = GetCharacter(nt, y, x);

						DakutenType dakutenType = GetDakutenType(value);
						if(dakutenType == DakutenType.None) {
							bool isKana = (
								(value[0] >= '\x3041' && value[0] <= '\x3096') || //hiragana
								(value[0] >= '\x30A1' && value[0] <= '\x30FA') //katakana
							);

							DakutenType effectiveDakuten = DakutenType.None;
							if(previousLineDakutenType[x] != DakutenType.None) {
								effectiveDakuten = previousLineDakutenType[x];
							} else if(isKana) {
								effectiveDakuten = GetDakutenType(GetCharacter(nt, y, x + 1));
								if(effectiveDakuten != DakutenType.None && x < 31) {
									//Skip next character, to avoid using it for the line below
									previousLineDakutenType[x + 1] = DakutenType.None;
									x++;
								}
							}

							if(isKana && effectiveDakuten == DakutenType.Dakuten) {
								lineOutput.Append((char)(value[0] + 1));
							} else if(isKana && effectiveDakuten == DakutenType.Handakuten) {
								lineOutput.Append((char)(value[0] + 2));
							} else {
								lineOutput.Append(value);
							}
						}
						previousLineDakutenType[x] = dakutenType;
					}

					string rowString = lineOutput.ToString().Trim();
					if(rowString.Length > 0) {
						output.AppendLine(rowString);
					}
				}
			}

			string newText = output.ToString();
			if(newText != _prevText) {
				txtSelectedText.Text = newText;
				if(chkAutoCopyToClipboard.Checked && !string.IsNullOrWhiteSpace(newText)) {
					try {
						Clipboard.SetText(newText);
						_prevText = newText;
					} catch {
						//This can sometime fail if another program is trying to use the clipboard at the same time
					}
				} else {
					_prevText = newText;
				}
			}
		}

		private string GetMappedCharacter(string key)
		{
			string value;
			if(this._charMappings.TryGetValue(key, out value)) {
				return value;
			} else {
				return " ";
			}
		}

		private DakutenType GetDakutenType(string value)
		{
			if(value == "daku" || value == "ﾞ") {
				return DakutenType.Dakuten;
			} else if(value == "han" || value == "ﾟ") {
				return DakutenType.Handakuten;
			} else {
				return DakutenType.None;
			}
		}

		private string GetTileKey(int nametableIndex, int index)
		{
			byte tileIndex = _tileData[nametableIndex][index];

			for(int i = 0; i < 16; i++) {
				_tmpTileData[i] = _ppuMemory[_state.PPU.ControlFlags.BackgroundPatternAddr + tileIndex * 16 + i];
			}

			return ctrlCharacterMapping.GetColorIndependentKey(_tmpTileData);
		}

		private bool IgnoreTile(int nametableIndex)
		{
			if(chkIgnoreMirroredNametables.Checked) {
				switch(_state.Cartridge.Mirroring) {
					case MirroringType.ScreenAOnly:
					case MirroringType.ScreenBOnly:
						if(nametableIndex > 0) {
							return true;
						}
						break;

					case MirroringType.Horizontal:
						if((nametableIndex & 0x01) == 0x01) {
							return true;
						}
						break;

					case MirroringType.Vertical:
						if((nametableIndex & 0x02) == 0x02) {
							return true;
						}
						break;
				}
			}
			return false;
		}

		public void SetCharacterMappings(ConcurrentDictionary<string, string> charMappings)
		{
			_charMappings = charMappings;
		}

		private void GetIndexes(int inNt, int inY, int inX, out int outNt, out int outY, out int outX)
		{
			outX = inX;
			outY = inY;
			outNt = inNt & 0x03;
			
			if(chkUseScrollOffsets.Checked) {
				outY += _yScroll / 8;
				outX += _xScroll / 8;
			}

			while(outX < 0) {
				outX += 32;
				outNt ^= 1;
			}
			
			while(outX >= 32) {
				outX -= 32;
				outNt ^= 1;
			}

			while(outY >= 30) {
				outY -= 30;
				outNt ^= 2;
			}

			while(outY < 0) {
				outY += 30;
				outNt ^= 2;
			}

			outNt &= 0x03;
		}

		private void chkAutoCopyToClipboard_CheckedChanged(object sender, EventArgs e)
		{
			if(chkAutoCopyToClipboard.Checked && !string.IsNullOrWhiteSpace(_prevText)) {
				try {
					Clipboard.SetText(_prevText);
				} catch {
					//This can sometime fail if another program is trying to use the clipboard at the same time
				}
			}
		}

		/// <summary>
		/// 处理 ActiveTranslateCheckBox 的勾选状态变化
		/// </summary>
		private void ActiveTranslateCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			UpdateTextBoxVisibility();

			if(ActiveTranslateCheckBox.Checked) {
				// 启用翻译功能
				_translateTimer.Start();
				btnManualTranslate.Visible = true;
				UpdateCountdownButton();
				// 立即执行一次翻译检查
				TranslateTimer_Tick(null, null);
			} else {
				// 禁用翻译功能
				_translateTimer.Stop();
				btnManualTranslate.Visible = false;
				TranslateTextBox.Text = "";
				_lastTranslatedText = "";
				_isTranslating = false;
			}
		}

		/// <summary>
		/// 根据勾选状态切换文本框显示
		/// </summary>
		private void UpdateTextBoxVisibility()
		{
			bool isActive = ActiveTranslateCheckBox.Checked;
			// 勾选时仅显示翻译框
			TranslateTextBox.Visible = isActive;
			TranslateTextBox.Enabled = isActive;
			txtSelectedText.Visible = !isActive;
			txtSelectedText.Enabled = !isActive;
		}

		/// <summary>
		/// 处理手动翻译按钮点击事件
		/// </summary>
		private async void btnManualTranslate_Click(object sender, EventArgs e)
		{
			// 立即执行翻译
			await PerformTranslation();
			// 重置倒计时时间
			_lastTranslateTime = DateTime.Now;
		}

		/// <summary>
		/// 定时器 Tick 事件处理 - 更新倒计时显示并检查是否需要翻译
		/// </summary>
		private async void TranslateTimer_Tick(object sender, EventArgs e)
		{
			// 更新倒计时按钮显示
			UpdateCountdownButton();
			
			// 检查是否到了翻译时间
			if(DateTime.Now.Subtract(_lastTranslateTime).TotalSeconds >= TranslateIntervalSeconds) {
				await PerformTranslation();
			}
		}

		/// <summary>
		/// 更新倒计时按钮的显示文本
		/// </summary>
		private void UpdateCountdownButton()
		{
			if(!ActiveTranslateCheckBox.Checked || !btnManualTranslate.Visible) {
				return;
			}

			double remainingSeconds = TranslateIntervalSeconds - DateTime.Now.Subtract(_lastTranslateTime).TotalSeconds;
			if(remainingSeconds <= 0) {
				btnManualTranslate.Text = "立即翻译";
			} else {
				btnManualTranslate.Text = $"立即翻译 ({Math.Ceiling(remainingSeconds)}s)";
			}
		}

		/// <summary>
		/// 执行翻译操作
		/// </summary>
		private async Task PerformTranslation()
		{
			// 如果正在翻译中，跳过本次检查
			if(_isTranslating) {
				return;
			}

			string currentText = txtSelectedText.Text?.Trim();
			
			// 检查文本是否满足翻译条件
			if(string.IsNullOrEmpty(currentText) || 
			   currentText.Length <= 2 || 
			   currentText == _lastTranslatedText) {
				return;
			}

			// 开始翻译
			_isTranslating = true;
			TranslateTextBox.Text = "翻译中...";
			_lastTranslateTime = DateTime.Now; // 更新翻译时间
			
			try {
				// 调用 AI 翻译服务
				string translatedText = await AiTranslator.TranslateAsync(currentText);
				translatedText = translatedText.Replace("\n", "\r\n");
				// 更新翻译结果
				if(!string.IsNullOrEmpty(translatedText)) {
					TranslateTextBox.Text = translatedText;
					_lastTranslatedText = currentText;
				} else {
					TranslateTextBox.Text = "翻译失败";
				}
			} catch(Exception ex) {
				// 翻译出错时的处理
				TranslateTextBox.Text = $"翻译出错: {ex.Message}";
			} finally {
				_isTranslating = false;
			}
		}
	}

	enum DakutenType {
		None = 0,
		Dakuten = 1,
		Handakuten = 2
	}
}

﻿namespace Mesen.GUI.Debugger.Controls
{
	partial class ctrlTextHooker
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.picNametable = new System.Windows.Forms.PictureBox();
            this.grpText = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.chkAutoCopyToClipboard = new System.Windows.Forms.CheckBox();
            this.chkIgnoreMirroredNametables = new System.Windows.Forms.CheckBox();
            this.btnClearSelection = new System.Windows.Forms.Button();
            this.txtSelectedText = new System.Windows.Forms.TextBox();
            this.chkUseScrollOffsets = new System.Windows.Forms.CheckBox();
            this.TranslateTextBox = new System.Windows.Forms.TextBox();
            this.ActiveTranslateCheckBox = new System.Windows.Forms.CheckBox();
            this.btnManualTranslate = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picNametable)).BeginInit();
            this.grpText.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.picNametable, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.grpText, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1054, 453);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // picNametable
            // 
            this.picNametable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picNametable.Location = new System.Drawing.Point(4, 4);
            this.picNametable.Margin = new System.Windows.Forms.Padding(4);
            this.picNametable.Name = "picNametable";
            this.picNametable.Size = new System.Drawing.Size(514, 445);
            this.picNametable.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picNametable.TabIndex = 0;
            this.picNametable.TabStop = false;
            // 
            // grpText
            // 
            this.grpText.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpText.Controls.Add(this.tableLayoutPanel2);
            this.grpText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpText.Location = new System.Drawing.Point(525, 3);
            this.grpText.Name = "grpText";
            this.grpText.Size = new System.Drawing.Size(526, 447);
            this.grpText.TabIndex = 4;
            this.grpText.TabStop = false;
            this.grpText.Text = "Text Hooker";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.chkAutoCopyToClipboard, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.chkIgnoreMirroredNametables, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.btnClearSelection, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.txtSelectedText, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.chkUseScrollOffsets, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.TranslateTextBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.ActiveTranslateCheckBox, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnManualTranslate, 1, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(520, 427);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // chkAutoCopyToClipboard
            // 
            this.chkAutoCopyToClipboard.AutoSize = true;
            this.chkAutoCopyToClipboard.Location = new System.Drawing.Point(3, 317);
            this.chkAutoCopyToClipboard.Name = "chkAutoCopyToClipboard";
            this.chkAutoCopyToClipboard.Size = new System.Drawing.Size(156, 16);
            this.chkAutoCopyToClipboard.TabIndex = 7;
            this.chkAutoCopyToClipboard.Text = "Auto-copy to clipboard";
            this.chkAutoCopyToClipboard.UseVisualStyleBackColor = true;
            this.chkAutoCopyToClipboard.CheckedChanged += new System.EventHandler(this.chkAutoCopyToClipboard_CheckedChanged);
            // 
            // chkIgnoreMirroredNametables
            // 
            this.chkIgnoreMirroredNametables.AutoSize = true;
            this.chkIgnoreMirroredNametables.Location = new System.Drawing.Point(3, 339);
            this.chkIgnoreMirroredNametables.Name = "chkIgnoreMirroredNametables";
            this.chkIgnoreMirroredNametables.Size = new System.Drawing.Size(180, 16);
            this.chkIgnoreMirroredNametables.TabIndex = 6;
            this.chkIgnoreMirroredNametables.Text = "Ignore mirrored nametables";
            this.chkIgnoreMirroredNametables.UseVisualStyleBackColor = true;
            // 
            // btnClearSelection
            // 
            this.btnClearSelection.Location = new System.Drawing.Point(3, 383);
            this.btnClearSelection.Name = "btnClearSelection";
            this.btnClearSelection.Size = new System.Drawing.Size(92, 21);
            this.btnClearSelection.TabIndex = 0;
            this.btnClearSelection.Text = "Clear Selection";
            this.btnClearSelection.UseVisualStyleBackColor = true;
            this.btnClearSelection.Visible = false;
            // 
            // txtSelectedText
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.txtSelectedText, 2);
            this.txtSelectedText.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtSelectedText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSelectedText.Location = new System.Drawing.Point(3, 3);
            this.txtSelectedText.Multiline = true;
            this.txtSelectedText.Name = "txtSelectedText";
            this.txtSelectedText.ReadOnly = true;
            this.txtSelectedText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSelectedText.Size = new System.Drawing.Size(514, 308);
            this.txtSelectedText.TabIndex = 1;
            // 
            // chkUseScrollOffsets
            // 
            this.chkUseScrollOffsets.AutoSize = true;
            this.chkUseScrollOffsets.Location = new System.Drawing.Point(3, 361);
            this.chkUseScrollOffsets.Name = "chkUseScrollOffsets";
            this.chkUseScrollOffsets.Size = new System.Drawing.Size(240, 16);
            this.chkUseScrollOffsets.TabIndex = 2;
            this.chkUseScrollOffsets.Text = "Adjust viewport by scrolling offsets";
            this.chkUseScrollOffsets.UseVisualStyleBackColor = true;
            // 
            // TranslateTextBox
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.TranslateTextBox, 2);
            this.TranslateTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TranslateTextBox.Location = new System.Drawing.Point(3, 3);
            this.TranslateTextBox.Multiline = true;
            this.TranslateTextBox.Name = "TranslateTextBox";
            this.TranslateTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TranslateTextBox.WordWrap = true;
            this.TranslateTextBox.Size = new System.Drawing.Size(514, 308);
            this.TranslateTextBox.TabIndex = 8;
            this.TranslateTextBox.Visible = false;
            // 
            // ActiveTranslateCheckBox
            // 
            this.ActiveTranslateCheckBox.AutoSize = true;
            this.ActiveTranslateCheckBox.Location = new System.Drawing.Point(249, 317);
            this.ActiveTranslateCheckBox.Name = "ActiveTranslateCheckBox";
            this.ActiveTranslateCheckBox.Size = new System.Drawing.Size(120, 16);
            this.ActiveTranslateCheckBox.TabIndex = 9;
            this.ActiveTranslateCheckBox.Text = "Active Translate";
            this.ActiveTranslateCheckBox.UseVisualStyleBackColor = true;
            this.ActiveTranslateCheckBox.CheckedChanged += new System.EventHandler(this.ActiveTranslateCheckBox_CheckedChanged);
            // 
            // btnManualTranslate
            // 
            this.btnManualTranslate.Location = new System.Drawing.Point(249, 339);
            this.btnManualTranslate.Name = "btnManualTranslate";
            this.btnManualTranslate.Size = new System.Drawing.Size(120, 23);
            this.btnManualTranslate.TabIndex = 10;
            this.btnManualTranslate.Text = "立即翻译 (5s)";
            this.btnManualTranslate.UseVisualStyleBackColor = true;
            this.btnManualTranslate.Visible = false;
            this.btnManualTranslate.Click += new System.EventHandler(this.btnManualTranslate_Click);
            // 
            // ctrlTextHooker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ctrlTextHooker";
            this.Size = new System.Drawing.Size(1054, 453);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picNametable)).EndInit();
            this.grpText.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.PictureBox picNametable;
		private System.Windows.Forms.GroupBox grpText;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button btnClearSelection;
		private System.Windows.Forms.CheckBox chkAutoCopyToClipboard;
		private System.Windows.Forms.CheckBox chkIgnoreMirroredNametables;
		private System.Windows.Forms.CheckBox chkUseScrollOffsets;
	  private System.Windows.Forms.TextBox txtSelectedText;
	  private System.Windows.Forms.TextBox TranslateTextBox;
	  private System.Windows.Forms.CheckBox ActiveTranslateCheckBox;
	  private System.Windows.Forms.Button btnManualTranslate;
   }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Configuration;

namespace Mesen.GUI
{
   internal class AiTranslator
   {
       private static readonly HttpClient _httpClient = new HttpClient();
       private const string ApiUrl = "https://open.bigmodel.cn/api/paas/v4/chat/completions";
       
       /// <summary>
       /// 从配置文件读取Bearer Token
       /// </summary>
       private static string BearerToken => ConfigurationManager.AppSettings["AiTranslatorBearerToken"] ?? string.Empty;

       /// <summary>
       /// 异步翻译文本内容
       /// </summary>
       /// <param name="text">要翻译的文本内容</param>
       /// <returns>翻译后的文本，如果失败则返回空字符串</returns>
       public static async Task<string> TranslateAsync(string text)
       {
           try
           {
               // 构建请求消息
               var requestMessage = new HttpRequestMessage(HttpMethod.Post, ApiUrl);
               requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);
               requestMessage.Content = new StringContent(
                   JsonSerializer.Serialize(new
                   {
                       model = "glm-4.5-flash",
                       messages = new[]
                       {
                           new
                           {
                               role = "system",
                               content = @"请把用户输入的内容，翻译成中文。不要有多余的回复。
                                                    对于没有实际意义的部分乱码，你可以直接忽略。
                                                    采用 原文-译文 对照的方式进行翻译。
                                                    如果原文不构成句子，翻译格式为 原文：译文。
                                                    对于构成句子的部分，一句原文对应一句译文。
													对于日文，有可能存在少量错误，你需要自己纠错之后再翻译。"
									},
                           new
                           {
                               role = "user",
                               content = text
                           }
                       },
                       temperature = 0.6,
                       stream = false,
                       thinking = new
                       {
                           type = "disabled"
                       }
                   }),
                   Encoding.UTF8,
                   "application/json"
               );

               // 发送请求并获取响应
               var response = await _httpClient.SendAsync(requestMessage);
               response.EnsureSuccessStatusCode();

               // 读取响应内容
               var responseContent = await response.Content.ReadAsStringAsync();

               // 解析JSON响应
               using (var jsonDoc = JsonDocument.Parse(responseContent))
               {
                   var root = jsonDoc.RootElement;

                   // 提取翻译结果
                   if (root.TryGetProperty("choices", out var choices) &&
                       choices.GetArrayLength() > 0 &&
                       choices[0].TryGetProperty("message", out var message) &&
                       message.TryGetProperty("content", out var content))
                   {
                       return content.GetString() ?? string.Empty;
                   }
               }

               return string.Empty;
           }
           catch (Exception ex)
           {
               return string.Empty;
           }
       }
   }
}

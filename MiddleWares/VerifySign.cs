using Microsoft.AspNetCore.Http;
using SocialMedia.Common;
using SocialMedia.Enum;
using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.MiddleWares
{
    public class VerifySign
    {
        private readonly RequestDelegate _next;
        //不用驗證的路由
        private List<string> NoVerifyPath =>new List<string> { "/login", "/register", "/personal/selectoption","/home","/" };

        //=> {get;} 同意
        public static string MD5Key => "jhkhjkqwkafjkcvkasdkfpewrerewr";
        public VerifySign(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            
            try
            {
                string path = context.Request.Path.ToString().ToLower();
                string username = context.Request.Query["username"].ToString();
                string token = context.Request.Query["token"].ToString();
                //允許body 多次使用 .net core 的坑
                context.Request.EnableBuffering();
                if (context.Request.Method == "POST")
                {

                    string requestContent;
                    var reqbody = context.Request.Body;
                    var reader = new StreamReader(reqbody); //這邊有bug 用using 會報錯， cannot access dispose obj               
                    requestContent = await reader.ReadToEndAsync();
                    context.Request.Body.Seek(0, SeekOrigin.Begin);
                    //context.Request.Body.Position = 0;
                    

                    Dictionary<string, object> dic = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(requestContent);


                    username = dic["username"].ToString();
                    token = dic["token"].ToString();
                    
                }
                
                //不用驗證的路由
                //string[] noVerifyPath = new string[] { "/login","/register" ,"/personal/selectoption" };
                if (!NoVerifyPath.Contains(path))
                {
                    //簽名失敗得話
                    if (!Verify(username, token))
                    {
                        var resp = new BasicResp
                        {
                            code = (int)RespCode.FAIL,
                            msg = "簽名失敗"
                        };
                        string respstr = System.Text.Json.JsonSerializer.Serialize(resp);
                        await context.Response.WriteAsync(respstr);
                    }
                }

                await _next(context);
            }
            catch (Exception ex)
            {

            }
            
            
        }

        //簽名方法
        public static bool Verify(string username, string token)
        {
            string sign = GenerateSign(username);
            //true or false
            return string.Equals(sign, token);
        }

        public static string GenerateSign(string username)
        {
            string signsource = $"username={username}&key={MD5Key}";
            string sign = MD5util.Hash(signsource);
            return sign;
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using SocialMedia.Common;
using SocialMedia.Interface;
using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Controllers
{
    public class LoginController : BaseController
    {
        //注入login服務
        private readonly ILogin _login;

        private readonly ILogMan _logMan;
        public LoginController(ILogin login, ILogMan logman):base(logman)
        {
            _login = login;
            _logMan=logman;
        }
        public async Task<IActionResult> Index([FromBody] LoginReq reqbody)
        {
            _logMan.Appendline($"LoginReq :{JsonUtil.Serialize(reqbody)}");
            var checkresult = await _login.CheckUserExisted(reqbody);
            _logMan.Appendline($"Resp : {JsonUtil.Serialize(checkresult)}");
            _logMan.WriteToFile();
            return RespResult(checkresult);
        }
    }
}

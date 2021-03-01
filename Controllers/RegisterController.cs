using Microsoft.AspNetCore.Mvc;
using SocialMedia.Common;
using SocialMedia.Dbcontext;
using SocialMedia.Interface;
using SocialMedia.Models;
using SocialMedia.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Controllers
{
    public class RegisterController : BaseController
    {
        //注入register 服務
        private readonly IRegister _register;
        private readonly ILogMan _logMan;
        public RegisterController(IRegister register, ILogMan logMan) :base(logMan)
        {
            _register = register;
            _logMan = logMan;
        }


        //post
        public async Task<IActionResult> Index([FromBody] RegisReq reqbody)
        {
            _logMan.Appendline($"RegisReq :{JsonUtil.Serialize(reqbody)}");
            var checkresult=await  _register.CheckUserExisted(reqbody);
            return RespResult(checkresult);
        }
    }
}

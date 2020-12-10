using Microsoft.AspNetCore.Mvc;
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
    public class RegisterController : Controller
    {
        //注入register 服務
        private readonly IRegister _register;
        public RegisterController(IRegister register)
        {
            _register = register;
        }


        //post
        public IActionResult Index([FromBody] RegisReq reqbody)
        {
            var checkresult=_register.CheckUserExisted(reqbody);
            return Json(checkresult);
        }
    }
}

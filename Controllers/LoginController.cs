using Microsoft.AspNetCore.Mvc;
using SocialMedia.Interface;
using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Controllers
{
    public class LoginController : Controller
    {
        //注入login服務
        private readonly ILogin _login;
        public LoginController(ILogin login)
        {
            _login = login;
        }
        public IActionResult Index([FromBody] LoginReq reqbody)
        {
            var checkresult = _login.CheckUserExisted(reqbody);
            return Json(checkresult);
        }
    }
}

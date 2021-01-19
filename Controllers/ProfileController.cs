using Microsoft.AspNetCore.Mvc;
using SocialMedia.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Controllers
{
    public class ProfileController : Controller
    {
        //注入login服務
        private readonly IProfile _profile;
        public ProfileController(IProfile profile)
        {
            _profile = profile;
        }

        //獲取用戶訊息
        public IActionResult Index()
        {
            int memberID = Convert.ToInt32(Request.Query["memberid"]);
            string username = Request.Query["username"].ToString();

            var memberInfo = _profile.GetMemberDetail(memberID, username);
            return Json(memberInfo);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SocialMedia.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Controllers
{
    public class ProfileController : BaseController
    {
        //注入login服務
        private readonly IProfile _profile;

        private readonly ILogMan _logMan;
        public ProfileController(IProfile profile,ILogMan logMan):base(logMan)
        {
            _profile = profile;
            _logMan = logMan;
        }

        //獲取用戶訊息
        public async Task<IActionResult> Index()
        {
            int memberID = Convert.ToInt32(Request.Query["memberid"]);

            _logMan.Appendline($"memberid : {memberID}");
            //這邊用的是username
            string username = Request.Query["userprofile"].ToString();

            _logMan.Appendline($"username : {username}");

            var memberInfo =await _profile.GetMemberDetail(memberID, username);
            return RespResult(memberInfo);
        }
    }
}

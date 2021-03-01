using Microsoft.AspNetCore.Mvc;
using SocialMedia.Common;
using SocialMedia.Interface;
using SocialMedia.Models.DbModels;
using SocialMedia.Models.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Controllers
{
    public class MemberInfoController : BaseController
    {
        private readonly IMemberinfo _setting;

        private readonly ILogMan _logMan;

        public MemberInfoController(IMemberinfo setting, ILogMan logman):base(logman)
        {
            _setting = setting;
            _logMan = logman;
        }

        //get
        //用戶列表
        //public IActionResult Index([FromBody] SetReq reqbody)
        //{
        //    var checkresult = _setting.GetMemberInfo(reqbody.memberid);
        //    return Json(checkresult);
        //}

        //get
        //用戶列表
        public async Task<IActionResult> Index()
        {
            int memberID = Convert.ToInt32(Request.Query["memberid"]);
            //var checkresult = _setting.GetMemberInfo(reqbody.memberid);
            _logMan.Appendline($"memberid : {memberID}");
            var checkresult =await _setting.GetMemberList(memberID);
            return RespResult(checkresult);
        }

       

        //test 新增 選項

        public IActionResult AddInter([FromBody] Interest interest)
        {
            var result = _setting.AddInterest1(interest);
            return Json(result);
        }

        public IActionResult AddPer([FromBody] Personality personality)
        {
            var result = _setting.AddPersonality1(personality);
            return Json(result);
        }
    }
}

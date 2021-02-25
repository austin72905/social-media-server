using Microsoft.AspNetCore.Mvc;
using SocialMedia.Interface;
using SocialMedia.Models.DbModels;
using SocialMedia.Models.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Controllers
{
    public class MemberInfoController : Controller
    {
        private readonly IMemberinfo _setting;

        public MemberInfoController(IMemberinfo setting)
        {
            _setting = setting;
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
            var checkresult =await _setting.GetMemberList(memberID);
            return Json(checkresult);
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

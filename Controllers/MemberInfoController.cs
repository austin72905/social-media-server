using Microsoft.AspNetCore.Mvc;
using SocialMedia.Interface;
using SocialMedia.Models.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Controllers
{
    public class MemberInfoController : Controller
    {
        private readonly ISetting _setting;

        public MemberInfoController(ISetting setting)
        {
            _setting = setting;
        }

        //get
        public IActionResult Index(int? id)
        {
            var checkresult = _setting.GetMemberInfo(id);
            return Json(checkresult);
        }

        //post 
        public IActionResult Post([FromBody] SetReq reqbody)
        {
            var result = _setting.GetMemberInfo(reqbody);
            return Json(result);
        }

        //取得選項
        //get
        public IActionResult SelectOption()
        {
            var result = _setting.GetSelectOption();
            return Json(result);
        }
    }
}

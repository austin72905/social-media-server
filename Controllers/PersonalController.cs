using Microsoft.AspNetCore.Mvc;
using SocialMedia.Common;
using SocialMedia.Interface;
using SocialMedia.Models.Personal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Controllers
{
    public class PersonalController : BaseController
    {
        //注入personal服務
        private readonly IPersonal _personal;
        private readonly ILogMan _logMan;
        public PersonalController(IPersonal personal, ILogMan logMan):base(logMan)
        {
            _personal = personal;
            _logMan = logMan;
        }

        //個人訊息
        public async Task<IActionResult> Index()
        {
            int memberID = Convert.ToInt32(Request.Query["memberid"]);
            _logMan.Appendline($"memberid : {memberID}");
            var memberInfo =await _personal.GetPersonalInfo(memberID);
            return RespResult(memberInfo);
        }

        //選項
        public async Task<IActionResult> SelectOption()
        {
            var selectOption =await _personal.GetSelectOption();
            return RespResult(selectOption);
        }

        public async Task<IActionResult> Update([FromBody] PersonalReq req)
        {
            _logMan.Appendline($"PersonalReq :{JsonUtil.Serialize(req)}");
            var memberInfo =await _personal.UpdatePersonalInfo(req);
            return RespResult(memberInfo);
        }

    }
}

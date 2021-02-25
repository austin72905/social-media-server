using Microsoft.AspNetCore.Mvc;
using SocialMedia.Interface;
using SocialMedia.Models.Personal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Controllers
{
    public class PersonalController : Controller
    {
        //注入personal服務
        private readonly IPersonal _personal;
        public PersonalController(IPersonal personal)
        {
            _personal = personal;
        }

        //個人訊息
        public async Task<IActionResult> Index()
        {
            int memberID = Convert.ToInt32(Request.Query["memberid"]);
            var memberInfo =await _personal.GetPersonalInfo(memberID);
            return Json(memberInfo);
        }

        //選項
        public async Task<IActionResult> SelectOption()
        {
            var selectOption =await _personal.GetSelectOption();
            return Json(selectOption);
        }

        public async Task<IActionResult> Update([FromBody] PersonalReq req)
        {

            var memberInfo =await _personal.UpdatePersonalInfo(req);
            return Json(memberInfo);
        }

    }
}

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
        public IActionResult Index()
        {
            int memberID = Convert.ToInt32(Request.Query["memberid"]);
            var memberInfo = _personal.GetPersonalInfo(memberID);
            return Json(memberInfo);
        }

        //選項
        public IActionResult SelectOption()
        {
            var selectOption = _personal.GetSelectOption();
            return Json(selectOption);
        }

        public IActionResult Update([FromBody] PersonalReq req)
        {

            var memberInfo = _personal.UpdatePersonalInfo(req);
            return Json(memberInfo);
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using SocialMedia.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Controllers
{
    public class MessageController : Controller
    {
        //注入login服務
        private readonly IMessage _message;
        public MessageController(IMessage message)
        {
            _message = message;
        }
        public IActionResult Index()
        {
            string memberid = Request.Query["memberid"].ToString();
            string recieveid = Request.Query["recieveid"].ToString();
            var result = _message.GetMsg(memberid, recieveid);
            return Json(result);
        }

        public IActionResult LastMsgs()
        {
            string memberid = Request.Query["memberid"].ToString();
            return Json("");
        }

        public IActionResult UnreadMsg()
        {
            string memberid = Request.Query["memberid"].ToString();
            int countUnread = 0;
            return Json("");
        }
    }
}

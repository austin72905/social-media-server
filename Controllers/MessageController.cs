using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            string memberid = Request.Query["memberid"].ToString();
            string recieveid = Request.Query["recieveid"].ToString();
            return Json("");
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

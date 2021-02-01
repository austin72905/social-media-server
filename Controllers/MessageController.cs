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
            var result = _message.GetAllLastMsg(memberid);
            return Json(result);
        }

        public IActionResult UnreadMsg()
        {
            //因為要跟message 組件比對，不能直接返回int
            //要隨時計算總和，計算給錢端坐，後端傳資料就好了
            string memberid = Request.Query["memberid"].ToString();
            var countUnread = _message.GetUnreadMsg(memberid);
            return Json(countUnread);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SocialMedia.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Controllers
{
    public class MessageController : BaseController
    {
        //注入login服務
        private readonly IMessage _message;
        private readonly ILogMan _logMan;
        public MessageController(IMessage message, ILogMan logMan):base(logMan)
        {
            _message = message;
            _logMan = logMan;
        }
        public async Task<IActionResult> Index()
        {
            string memberid = Request.Query["memberid"].ToString();
            string recieveid = Request.Query["recieveid"].ToString();
            _logMan.Appendline($"memberid : {memberid}");
            _logMan.Appendline($"recieveid : {recieveid}");

            var result =await  _message.GetMsg(memberid, recieveid);
            return RespResult(result);
        }

        public async Task<IActionResult> LastMsgs()
        {
            string memberid = Request.Query["memberid"].ToString();
            _logMan.Appendline($"memberid : {memberid}");
            var result =await _message.GetAllLastMsg(memberid);
            return RespResult(result);
        }

        public async Task<IActionResult> UnreadMsg()
        {
            //因為要跟message 組件比對，不能直接返回int
            //要隨時計算總和，計算給錢端坐，後端傳資料就好了
            string memberid = Request.Query["memberid"].ToString();
            _logMan.Appendline($"memberid : {memberid}");
            var countUnread =await _message.GetUnreadMsg(memberid);
            return RespResult(countUnread);
        }
    }
}

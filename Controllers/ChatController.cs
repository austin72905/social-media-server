using Microsoft.AspNetCore.Mvc;
using SocialMedia.Interface;
using SocialMedia.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChat _chat;

        public ChatController(IChat chat)
        {
            _chat = chat;
        }

        public async Task<IActionResult> Index()
        {
            var result =await _chat.GetChatMemList();
            return Json(result);
        }

        //寫個接口 更新資料庫
        //1. 進入聊天室 => 改成已讀
        public async Task<IActionResult> UpToread()
        {
            string memberid = Request.Query["memberid"].ToString();
            string receiveid = Request.Query["recieveid"].ToString();
            await _chat.UpdateToRead(memberid, receiveid);
            return Json("");
        }

        //2. 發送訊息時 => 將訊息存入資料庫
        public async Task<IActionResult> SaveMsgs([FromBody] ChatReq req)
        {
            await _chat.SaveMsgData(req.memberid, req.recieveid, req.input);
            return Json("");
        }


    }
}

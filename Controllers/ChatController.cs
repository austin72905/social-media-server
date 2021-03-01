using Microsoft.AspNetCore.Mvc;
using SocialMedia.Common;
using SocialMedia.Interface;
using SocialMedia.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Controllers
{
    public class ChatController : BaseController
    {
        private readonly IChat _chat;

        private readonly ILogMan _logMan;

        public ChatController(IChat chat,ILogMan logMan):base(logMan)
        {
            _chat = chat;
            _logMan = logMan;
        }

        public async Task<IActionResult> Index()
        {
            var result =await _chat.GetChatMemList();
            //_logMan.Appendline($"Resp : {JsonUtil.Serialize(result)}");
            //_logMan.WriteToFile();
            return RespResult(result);
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

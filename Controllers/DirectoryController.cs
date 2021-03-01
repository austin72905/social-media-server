using Microsoft.AspNetCore.Mvc;
using SocialMedia.Common;
using SocialMedia.Interface;
using SocialMedia.Models.Friends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Controllers
{
    public class DirectoryController : BaseController
    {
        //注入login服務
        private readonly IDirectory _directory;
        private readonly ILogMan _logMan;
        public DirectoryController(IDirectory directory, ILogMan logMan):base(logMan)
        {
            _directory = directory;
            _logMan = logMan;
        }
        //get
        public async Task<IActionResult> Index()
        {
            int memberID = Convert.ToInt32(Request.Query["memberid"]);
            _logMan.Appendline($"memberid : {memberID}");
            var checkresult =await  _directory.GetFrinedList(memberID);
            return RespResult(checkresult);
        }

        //新增好友
        public async Task<IActionResult> Add([FromBody] FriendReq req)
        {
            _logMan.Appendline($"FriendReq : {JsonUtil.Serialize(req)}");
            var checjresult =await _directory.AddFrined(req);
            return RespResult(checjresult);
        }

        //刪除好友
        public async Task<IActionResult> Delete([FromBody] FriendReq req)
        {
            _logMan.Appendline($"FriendReq : {JsonUtil.Serialize(req)}");
            var checjresult =await _directory.DeleteFrined(req);
            return RespResult(checjresult);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SocialMedia.Interface;
using SocialMedia.Models.Friends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Controllers
{
    public class FriendController : Controller
    {
        private readonly IDirectory _directory;

        public FriendController(IDirectory directory)
        {
            _directory = directory;
        }

        //get 好友列表
        public IActionResult Index()
        {
            int memberID = Convert.ToInt32(Request.Query["memberid"]);
            var result=_directory.GetFrinedList(memberID);
            return Json(result);
        }

        //新增好友
        public IActionResult Add([FromBody] FriendReq req)
        {
            var checjresult = _directory.AddFrined(req);
            return Json(checjresult);
        }

        //刪除好友
        public IActionResult Delete([FromBody] FriendReq req)
        {
            var checjresult = _directory.DeleteFrined(req);
            return Json(checjresult);
        }
    }
}

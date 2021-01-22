using Microsoft.AspNetCore.Mvc;
using SocialMedia.Interface;
using SocialMedia.Models.Friends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Controllers
{
    public class DirectoryController : Controller
    {
        //注入login服務
        private readonly IDirectory _directory;
        public DirectoryController(IDirectory directory)
        {
            _directory = directory;
        }
        //get
        public IActionResult Index()
        {
            int memberID = Convert.ToInt32(Request.Query["memberid"]);
            var checkresult = _directory.GetFrinedList(memberID);
            return Json(checkresult);
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

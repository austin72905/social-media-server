using Microsoft.AspNetCore.Mvc;
using SocialMedia.Interface;
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
        public IActionResult Index(int? id)
        {
            var checkresult = _directory.GetFrinedList(id);
            return Json(checkresult);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SocialMedia.Common;
using SocialMedia.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Controllers
{
    public class BaseController : Controller
    {
        private readonly ILogMan _logMan;
        public BaseController(ILogMan logMan)
        {
            _logMan = logMan;
        }
        protected virtual JsonResult RespResult(object data = null,bool logNoData=false)
        {
            //有些data 太多的就不要打印了..
            if (logNoData)
            {
                          
            }
            _logMan.Appendline($"Resp : {JsonUtil.Serialize(data)}");
            _logMan.WriteToFile();
            return Json(data);
        }
    }
}

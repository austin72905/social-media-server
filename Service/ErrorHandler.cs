using SocialMedia.Enum;
using SocialMedia.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SocialMedia.Service
{
    public class ErrorHandler: IErrorHandler
    {
        private readonly ILogMan _logMan;

        public ErrorHandler(ILogMan logMan)
        {
            _logMan=logMan;
        }
        public T SysError<T>(T resp, string errMsg)
        {

            Type entityType = typeof(T);
            PropertyInfo propInfoCode = entityType.GetProperty("code");
            PropertyInfo propInfoMsg = entityType.GetProperty("msg");

            propInfoCode.SetValue(resp, (int)RespCode.FAIL);
            propInfoMsg.SetValue(resp, "系統內部異常");
            _logMan.Appendline($"System Error : {errMsg}");
            //var vale=propInfo.GetValue(resp);
            return resp;
        }

        public void SysError(string errMsg)
        {
            _logMan.Appendline($"System Error : {errMsg}");
        }
    }
}

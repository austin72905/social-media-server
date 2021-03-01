using SocialMedia.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Common
{
    public class LogMan : ILogMan
    {
        //暫時存放log 訊息，
        //最後一口氣 用 Info 方法寫進去
        private StringBuilder TempLogMsg { get; }

        private string _logName;

        public LogMan()
        {
            TempLogMsg = new StringBuilder();
        }

        //給logName設定值

        public void SetLogName(string logName)
        {
            _logName = logName;
        }

        public void Appendline(string text)
        {
            TempLogMsg.AppendLine(text);
        }

        public void WriteToFile()
        {
            NLog.Logger log = NLog.LogManager.GetLogger(_logName);
            log.SetProperty("LogName", _logName);
            string msg = TempLogMsg.ToString();
            //寫入檔案
            log.Info(msg);
            TempLogMsg.Clear();

        }
    }
}

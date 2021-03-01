using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Interface
{
    public interface ILogMan
    {
        public void SetLogName(string logName);

        public void Appendline(string text);

        public void WriteToFile();
    }
}

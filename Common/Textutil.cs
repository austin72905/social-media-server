using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Common
{
    public class Textutil
    {
        //hex 16進位字串
        public static string BytesToHex(byte[] bytes)
        {
            var builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                //https://www.itread01.com/content/1548125287.html
                //ToString("x2") 用意
                //X為     十六進位制 
                //2為 每次都是兩位數
                //比如   0x0A ，若沒有2,就只會輸出0xA
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}

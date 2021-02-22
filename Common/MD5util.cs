using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Common
{
    //当对一个类应用 sealed 修饰符时，此修饰符会阻止其他类从该类继承
    public sealed class MD5util
    {

        //使用多型

        public static string Hash(string text)
        {
            return Hash(text, Encoding.UTF8);
        }

        public static string Hash(string text, Encoding encoding)
        {
            byte[] data = encoding.GetBytes(text);
            return Hash(data);
        }

        public static string Hash(byte[] data)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(data);
                return Textutil.BytesToHex(hashBytes);
            }
        }
    }
}

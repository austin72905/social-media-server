using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models.Login
{
    public class LoginResp
    {
        public  int code { get; set; }

        public  string msg { get; set; }
      
        public LoginData data { get; set; }

        public string token { get; set; }
    }

    public class LoginData
    {
        public string username { get; set; }
        public string gender { get; set; }
        public int memberID { get; set; }
        public bool isRegist { get; set; }
    }
}

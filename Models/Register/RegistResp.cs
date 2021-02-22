using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models.Register
{
    public class RegistResp
    {
        public int code { get; set; }

        public string token { get; set; }
        public RegistData data { get; set; }
        public string msg { get; set; }

    }

    public class RegistData
    {
        public string username { get; set; }
        public string gender { get; set; }
        public int memberID { get; set; }
        public bool isRegist { get; set; }
    }

}

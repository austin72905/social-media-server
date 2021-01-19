using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models.Personal
{
    public class PersonalResp
    {
        public int code { get; set; }
        public PersonalData data { get; set; }
        public string msg { get; set; }
    }

    public class PersonalData
    {
        public string username { get; set; }

        public string nickname { get; set; }

        public string gender { get; set; }
        public string job { get; set; }

        public string state { get; set; }

        public string introduce { get; set; }

        public string interest { get; set; }

        public string preferType { get; set; }

        public int memberID { get; set; }
        public bool isRegist { get; set; }
    }

}

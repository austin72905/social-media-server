using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models.Personal
{
    public class SOResp
    {
        public int code { get; set; }
        public SOData data { get; set; }
        public string msg { get; set; }
    }

    public class SOData
    {
        public List<string> interests { get; set; }

        public List<string> preferTypes { get; set; }

        public bool isRegist { get; set; }
    }

}

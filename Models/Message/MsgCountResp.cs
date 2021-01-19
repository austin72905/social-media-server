using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models.Message
{
    public class MsgCountResp
    {
        public int code { get; set; }
        public Dictionary<string, int> data { get; set; }
        public string msg { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models.Message
{
    public class MsgLastResp
    {
        public int code { get; set; }
        
        public List<ChatMsgLastData> data { get; set; }
        public string msg { get; set; }

    }
}

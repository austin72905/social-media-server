using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models.Message
{
    public class MsgResp
    {
        public int code { get; set; }
        
        public List<ChatMsgData> data { get; set; }
        public string msg { get; set; }
    }
}

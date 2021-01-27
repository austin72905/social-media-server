using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models.Message
{
    //聊天內容
    //傳給聊天室的
    public class ChatResp
    {
        public string username { get; set; }

        public int memberid { get; set; }

        public string gender { get; set; }
    }
}

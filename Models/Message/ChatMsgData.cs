using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models.Message
{
    public class ChatMsgData
    {
        public string username { get; set; }
        //哪個ID 的用戶傳的
        public int memberid { get; set; }

        public string gender { get; set; }

        public string text { get; set; }

        public bool unread { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models.Message
{
    public class ChatReq
    {
        public string memberid { get; set; }
        public string recieveid { get; set; }
        public string input { get; set; }
    }
}

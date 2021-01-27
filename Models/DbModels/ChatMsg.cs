using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models.DbModels
{
    public class ChatMsg
    {
        public int ID { get; set; }

        
        public int MemberID { get; set; }

        public int ChatID { get; set; } 

        public string Time { get; set; }
        //------------------------
        //Speaker 的名字
        //public string UserName { get; set; } =>resp 再傳 ，可以重其他表獲得
        //哪個ID 的用戶傳的
        public int SpeakerID { get; set; }
        //Speaker 的性別
        //public string Gender { get; set; } =>resp 再傳

        public string Text { get; set; }

        public bool Unread { get; set; }

        public Member Member { get; set; }
    }
}

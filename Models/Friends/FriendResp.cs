using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models.Friends
{
    public class FriendResp
    {
        public  int code { get; set; }

        public  string msg { get; set; }

        public List<FriendData> data { get; set; }
        
    }

    public class RespData
    {
        public string Gender { get; set; }
        //工作       
        public string Job { get; set; }
        //狀態       
        public string State { get; set; }
        //自我介紹      
        public string Introduce { get; set; }
        //興趣
        public string Intersert { get; set; }
        //偏好類型
        public string PreferType { get; set; }
        public bool IsRegist { get; set; }

    }

    public class FriendData
    {
        public string username { get; set; }

        public string nickname { get; set; }
        public int memberID { get; set; }
        public string gender { get; set; }
        public string job { get; set; }
        public string state { get; set; }
        public string introduce { get; set; }

        public List<string> intersert { get; set; }

        public List<string> preferType { get; set; }
        public bool isRegist { get; set; }
    }

}

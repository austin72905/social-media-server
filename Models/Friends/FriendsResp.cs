using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models.Friends
{
    public class FriendsResp : ResponseModel
    {
        public override int Code { get; set; }

        public override string Msg { get; set; }

        public List<RespData> data { get; set; }
        
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

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models.Setting
{
    public class SetReq : ReqInfoModel
    {
        public  string username { get; set; }

        public override int memberid { get; set; }

        public ReqData data { get; set; }
    }

    public class ReqData
    {
        public string Gender { get; set; }
        //工作       
        public string Job { get; set; }

        //工作       
        public string NickName { get; set; }

        //狀態       
        public string State { get; set; }
        //自我介紹      
        public string Introduce { get; set; }
        //興趣
        public List<string> Interests { get; set; }
        //偏好類型
        public List<string> PreferTypes { get; set; }

    }

}

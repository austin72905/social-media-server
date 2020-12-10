using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models.Setting
{
    public class SetResp : ResponseModel
    {
        public override int Code { get; set; }

        public override string Msg { get; set; }

        public List<RespData> data { get; set; }
    }

    public class RespData
    {
        
        public string MemberName { get; set; }
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


    //返回選項
    public class TypeResp : ResponseModel
    {
        public override int Code { get; set; }

        public override string Msg { get; set; }

        public OptionData OptionData { get; set; }

        public bool IsRegist { get; set; }

    }

    public class OptionData
    {
        public List<string> TypeData { get; set; }

        public List<string> Interestdata { get; set; }
    }

    

}

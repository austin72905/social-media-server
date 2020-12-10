using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models.Register
{
    public class RegistResp : ResponseModel
    {
        public override int Code { get; set; }

        public override string Msg { get; set; }

        public  Data data { get; set; }
     
    }

    public class Data
    {
        public string Gender { get; set; }

        public int MemberID { get; set; }

        public bool IsRegist { get; set; }
    }

}

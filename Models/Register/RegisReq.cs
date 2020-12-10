using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models
{
    public class RegisReq : RequestModel
    {
        public override string username { get; set; }

        public override string password { get; set; }

        public string gender { get; set; }
    }
}

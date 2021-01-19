using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models.Profile
{
    public class ProfileReq: RequestModel
    {
        public override string username { get; set; }
        public override string password { get; set; }   
    }
}

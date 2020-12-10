using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models
{
    public abstract class RequestModel
    {
        public abstract string username { get; set; }

        public abstract string password { get; set; }
    }

    public abstract class ReqInfoModel
    {
        public abstract int memberid { get; set; }
    }
}

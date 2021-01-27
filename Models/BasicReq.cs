using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models
{
    public abstract class BasicReq
    {
        public abstract string username { get; set; }

        public  abstract string password { get; set; }

    }
}

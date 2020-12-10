using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models
{
    public abstract class ResponseModel
    {
        public abstract int Code { get; set; }

        public abstract string Msg { get; set; }
        
    }

}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models
{
    public class LoginReq :BasicReq
    {
        public  override string username { get; set; }
        public override string password { get; set; }
    }
}

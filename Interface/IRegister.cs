﻿using SocialMedia.Models;
using SocialMedia.Models.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Interface
{
    public interface IRegister
    {
        RegistResp CheckUserExisted(RegisReq req);

    }
}

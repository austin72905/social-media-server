using SocialMedia.Models;
using SocialMedia.Models.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Interface
{
    public interface IRegister
    {
        Task<RegistResp> CheckUserExisted(RegisReq req);

    }
}

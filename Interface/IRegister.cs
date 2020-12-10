using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Interface
{
    public interface IRegister
    {
        ResponseModel CheckUserExisted(RegisReq req);

    }
}

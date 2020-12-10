using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Interface
{
    public interface ILogin
    {
        ResponseModel CheckUserExisted(LoginReq req);
    }
}

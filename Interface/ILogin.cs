using SocialMedia.Models;
using SocialMedia.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Interface
{
    public interface ILogin
    {
        LoginResp CheckUserExisted(LoginReq req);
    }
}

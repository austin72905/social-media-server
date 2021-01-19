using SocialMedia.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Interface
{
    public interface IProfile
    {
        public ProfileResp GetMemberDetail(int id, string username);

    }
}

using SocialMedia.Models.DbModels;
using SocialMedia.Models.Memberinfo;
using SocialMedia.Models.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Interface
{
    public interface IMemberinfo
    {
        //SetResp EditMemberInfo(SetReq req);

       // SetResp GetMemberInfo(int? id);

        //SetResp GetMemberList(int? id);

        //TypeResp GetSelectOption();


        MemberinfoResp GetMemberList(int id);
        object AddInterest1(Interest interest);

        object AddPersonality1(Personality personality);
    }
}

using SocialMedia.Models.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Interface
{
    public interface ISetting
    {
        SetResp GetMemberInfo(SetReq req);

        SetResp GetMemberInfo(int? id);

        SetResp GetMemberList(int? id);

        TypeResp GetSelectOption();
    }
}

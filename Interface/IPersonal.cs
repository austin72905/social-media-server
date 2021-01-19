using SocialMedia.Models.Personal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Interface
{
    public interface IPersonal
    {
        PersonalResp GetPersonalInfo(int id);

        PersonalResp UpdatePersonalInfo(PersonalReq req);
        SOResp GetSelectOption();
    }
}

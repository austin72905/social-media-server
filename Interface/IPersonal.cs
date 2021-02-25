using SocialMedia.Models.Personal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Interface
{
    public interface IPersonal
    {
        Task<PersonalResp> GetPersonalInfo(int id);

        Task<PersonalResp> UpdatePersonalInfo(PersonalReq req);
        Task<SOResp> GetSelectOption();
    }
}

using SocialMedia.Models.DbModels;
using SocialMedia.Models.Personal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Interface.Repository
{
    public interface IInterestRepository
    {
        public IQueryable<Interest> GetInterestInstance();

        public Task SaveMemberInterest(PersonalReq req);

    }
}

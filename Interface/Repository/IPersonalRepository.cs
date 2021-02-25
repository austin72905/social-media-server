using SocialMedia.Models.DbModels;
using SocialMedia.Models.Personal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Interface.Repository
{
    public interface IPersonalRepository
    {
        public IQueryable<Personality> GetPersonalInstance();

        public Task SavePreferType(PersonalReq req);
    }
}

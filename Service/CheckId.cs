using SocialMedia.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Service
{
    public static class CheckId
    {
        public static bool CheckMemberId(IQueryable<Member> context,int? id) 
        {
            var correctId = context.Any(m => m.ID == id);
            
            return correctId;
        }
    }
}

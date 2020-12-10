using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models.DbModels
{
    public class Interest
    {
        public int ID { get; set; }

        public string Name { get; set; }
        //導覽屬性
        public ICollection<MemberInterest> MemberInterests { get; set; }
    }
}

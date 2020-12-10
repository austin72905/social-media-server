using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models.DbModels
{
    public class Member
    {
        public int ID { get; set; }

        public string Name { get; set; }

        //導覽屬性       
        public Password Password { get; set; }
        public MemberInfo MemberInfo { get; set; }

        public Directory Directory { get; set; }

        public ICollection<PreferType> PreferTypes { get; set; }

        public ICollection<MemberInterest> MemberInterests { get; set; }
    } 
}

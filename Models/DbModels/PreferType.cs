using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models.DbModels
{
    public class PreferType
    {
        public int ID { get; set; }
        public int MemberID { get; set; }

        public int PersonalityID { get; set; }

        public Member Member { get; set; }

        public Personality Personality { get; set; }
    }
}

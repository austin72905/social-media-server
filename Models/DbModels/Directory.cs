using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models.DbModels
{
    public class Directory
    {
        public int ID { get; set; }
        public int MemberID { get; set; }

        public string ContactList { get; set; }

        public Member Member { get; set; }
    }
}

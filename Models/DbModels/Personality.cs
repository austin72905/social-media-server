using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models.DbModels
{
    public class Personality
    {
        public int ID { get; set; }

        public string Kind { get; set; }

        //導覽屬性
        public ICollection<PreferType> PreferTypes { get; set; }
    }
}

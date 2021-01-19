using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Models.Personal
{
    public class PersonalReq
    {
        public int memberid { get; set; }

        public UpdatePersonalData data { get; set; }
    }

    public class UpdatePersonalData
    {
        public string username { get; set; }

        public string nickname { get; set; }

        public string gender { get; set; }
        public string job { get; set; }

        public string state { get; set; }

        public string introduce { get; set; }

        public List<string> interest { get; set; }

        public List<string> preferType { get; set; }

        
    }
}

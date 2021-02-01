using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Chathub
{
    public  class ConnectionList
    {
        private static Dictionary<string, List<string>> _onnectList;
        //連線id 列表
        public static Dictionary<string, List<string>> ConnectList
        {
            get
            {
                if (_onnectList == null)
                {
                    var newdic = new Dictionary<string, List<string>>();
                    _onnectList = newdic;
                }
                return _onnectList;
            }
            set
            {

            }

        }
    }
}

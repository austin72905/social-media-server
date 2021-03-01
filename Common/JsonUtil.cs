using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocialMedia.Common
{
    public static class JsonUtil
    {
        public static string Serialize(object data)
        {
            return JsonSerializer.Serialize(data);
        }

        public static T Deserialize<T>(string jsonStr)
        {
            return JsonSerializer.Deserialize<T>(jsonStr);
        }
    }
}

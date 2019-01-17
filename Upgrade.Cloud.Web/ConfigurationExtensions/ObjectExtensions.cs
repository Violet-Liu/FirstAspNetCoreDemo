using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Upgrade.Cloud.Web.ConfigurationExtensions
{
    public static class ObjectExtensions
    {
        public static string ToJson<T>(this T t)
        {
            return JsonConvert.SerializeObject(t);
        }
    }
}

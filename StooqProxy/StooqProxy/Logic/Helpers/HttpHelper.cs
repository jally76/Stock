using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace StooqProxy.Logic.Helpers
{
    public static class HttpHelper
    {
        public static string GetWeb(string url)
        {
            return new HttpClient().GetAsync(url).Result.Content.ReadAsStringAsync().Result;
        }
    }
}

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
        /// <summary>
        /// Returns string of web at specified URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetWeb(string url)
        {
            return new HttpClient().GetAsync(url).Result.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        /// Returns string of web at specified URL concatenated with query postfix
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="query">Query postfix</param>
        /// <returns></returns>
        public static string GetWebQueried(string url, string query)
        {
            return GetWeb(string.Format("{0}{1}", url, query));
        }
    }
}

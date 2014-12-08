using HtmlAgilityPack;
using StockTools.BusinessLogic.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.BusinessLogic.Concrete
{
    public class BOSSAIntradayDataParser : IIntradayDataParser
    {
        public Dictionary<string, string> GetFileAddresses(string address)
        {
            var Webget = new HtmlWeb();
            var doc = Webget.Load(address);
            var result = new Dictionary<string, string>();

            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//table"))
            {

            }

            return result;
        }
    }
}

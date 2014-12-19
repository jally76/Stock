using HtmlAgilityPack;
using StockTools.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.Domain.Concrete
{
    public class BOSSAIntradayDataParser : IIntradayDataParser
    {
        public Dictionary<string, string> GetFileAddresses(string address)
        {
            var Webget = new HtmlWeb();
            var doc = Webget.Load(address);
            var result = new Dictionary<string, string>();

            foreach (HtmlNode row in doc.DocumentNode.SelectNodes("//table[@summary=' Archiwum']//tbody//tr"))
            {
                HtmlNodeCollection cells = row.SelectNodes("td");
                var name = cells[0].InnerText.Trim();
                //var date = cells[1].InnerText;
                var href = (from lnks in cells.Descendants()
                            where lnks.Name == "a" &&
                                 lnks.Attributes["href"] != null &&
                                 lnks.InnerText.Trim().Length > 0
                            select new
                            {
                                Url = lnks.Attributes["href"].Value.Trim(),
                                Text = lnks.InnerText.Trim()
                            }).SingleOrDefault().Url;
                result[name] = "http://bossa.pl" + href;
            }

            return result;
        }
    }
}

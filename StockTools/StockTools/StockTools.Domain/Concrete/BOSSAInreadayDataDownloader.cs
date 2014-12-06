using StockTools.BusinessLogic.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.BusinessLogic.Concrete
{
    public class BOSSAInreadayDataDownloader : IIntradayDataDownloader
    {
        public string Address { get; set; }
        public IIntradayDataParser Parser { get; set; }

        public void Download(string path)
        {
            var addresses = Parser.GetFileAddresses(Address);
            using (WebClient webClient = new WebClient())
            {
                foreach (var key in addresses.Keys)
                {
                    webClient.DownloadFile(
                        addresses[key], 
                        string.Format("{0}\\{1}", path, key));
                }
            }
        }
    }
}

﻿using StockTools.BusinessLogic.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.BusinessLogic.Concrete
{
    public class BOSSAIntradayDataDownloader : IIntradayDataDownloader
    {
        public string Address { get; set; }
        public IIntradayDataParser Parser { get; set; }

        public void DownloadToFile(string path)
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

        public Dictionary<string, byte[]> DownloadToMemory()
        {
            var addresses = Parser.GetFileAddresses(Address);
            Dictionary<string, byte[]> result = new Dictionary<string, byte[]>();
            foreach (var key in addresses.Keys)
            {
                WebRequest request = FtpWebRequest.Create(addresses[key]);
                using (WebResponse response = request.GetResponse())
                {
                    MemoryStream responseStream = response.GetResponseStream() as MemoryStream;
                    result[key] = responseStream.ToArray();
                }
            }
            return result;
        }
    }
}

using StockTools.BusinessLogic.Abstract;
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

        Queue<string> queue;
        Dictionary<string, string> addresses;
        string path;

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

        public void DownloadToFileParallel(string path)
        {
            this.path = path;
            addresses = Parser.GetFileAddresses(Address);
            queue = new Queue<string>();
            foreach (var key in addresses.Keys)
            {
                queue.Enqueue(key);
            }
            Parallel.ForEach(addresses.Keys, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount * 10 }, Download);
        }

        public void Download(string key)
        {
            //if (queue.Any())
            //{
            //    var key = queue.Dequeue();
                using (WebClient webClient = new WebClient())
                {
                    //webClient.DownloadStringCompleted += OnGetDownloadedStringCompleted;
                    //webClient.DownloadStringAsync(new Uri(nextItem));
                    //webClient.DownloadFileCompleted += webClient_DownloadFileCompleted;
                    //return;
                    webClient.DownloadFile(addresses[key], string.Format("{0}\\{1}", path, key));
                }
            //}
        }

        //void webClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        //{
        //    Download();
        //}

        public Dictionary<string, byte[]> DownloadToMemory()
        {
            var addresses = Parser.GetFileAddresses(Address);
            Dictionary<string, byte[]> result = new Dictionary<string, byte[]>();
            foreach (var key in addresses.Keys)
            {
                WebClient wc = new WebClient();
                using (MemoryStream stream = new MemoryStream(wc.DownloadData(addresses[key])))
                {
                    result[key] = stream.ToArray();
                }
            }
            return result;
        }
    }
}

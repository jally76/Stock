using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.BusinessLogic.Abstract
{
    public interface IIntradayDataDownloader
    {
        string Address { get; set; }
        IIntradayDataParser Parser { get; set; }

        void DownloadToFile(string path);
        void DownloadToFileParallel(string path);
        Dictionary<string, byte[]> DownloadToMemory();
    }
}

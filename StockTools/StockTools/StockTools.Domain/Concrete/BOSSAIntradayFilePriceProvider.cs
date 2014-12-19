using StockTools.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.Domain.Concrete
{
    public class BOSSAIntradayFilePriceProvider : ICurrentPriceProvider
    {
        string Path { get; set; }
        List<string> companies;

        public void LoadPrices(string path)
        {
            Path = path;
            //TODO Implement loading intraday BOSSA from files
            
            //Detecting available companies

            //Loading into memory?

            throw new NotImplementedException();
        }

        public void LoadPrices(Uri url)
        {
            throw new NotImplementedException();
        }

        public double GetPriceByShortName(string shortName)
        {
            throw new NotImplementedException();
        }

        public double GetPriceByFullName(string shortName)
        {
            throw new NotImplementedException();
        }

        public double GetPriceByShortNameAndDateTime(string shortName, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public double GetPriceByFullNameAndDateTime(string shortName, DateTime dateTime)
        {
            throw new NotImplementedException();
        }
    }
}

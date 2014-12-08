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
            throw new NotImplementedException();
        }
    }
}

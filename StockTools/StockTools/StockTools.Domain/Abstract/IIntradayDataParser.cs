using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockTools.BusinessLogic.Abstract
{
    public interface IIntradayDataParser
    {
        Dictionary<string,string> GetFileAddresses(string address);
    }
}

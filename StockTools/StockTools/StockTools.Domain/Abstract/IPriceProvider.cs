using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.Domain.Abstract
{
    public interface IPriceProvider
    {
        double GetPrice(string companyName, DateTime dateTime);
        Dictionary<DateTime, double> GetPrices(string companyName, DateTime day);
    }
}

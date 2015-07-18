using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.Data.HistoricalData
{
    public interface IHistoricalDataProvider
    {
        void AddPrice(Price price);
        Price GetPrice(string companyName, DateTime dateTime);

        void AddCompany(Company company);
        Company GetCompany(string name);
        
        List<Price> GetPriceListByDay(string name, DateTime dateTime);

        void Save();
    }
}

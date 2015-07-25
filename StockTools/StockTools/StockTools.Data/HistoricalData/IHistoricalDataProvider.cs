using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.Data.HistoricalData
{
    public interface IHistoricalDataProvider
    {
        void AddPrice(Price price);
        void AddRangePrice(List<Price> prices);
        Price GetPrice(string companyName, DateTime dateTime);

        void AddCompany(Company company);
        Company GetCompany(string name);
        List<Company> GetCompanies();

        List<Price> GetPriceListByDay(string name, DateTime dateTime);

        void Save();
    }
}

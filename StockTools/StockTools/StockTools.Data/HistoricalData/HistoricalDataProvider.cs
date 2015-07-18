using System;
using System.Collections.Generic;
using System.Linq;

namespace StockTools.Data.HistoricalData
{
    public class HistoricalDataProvider : IHistoricalDataProvider
    {
        public StockEntities DbContext { get; set; }

        public HistoricalDataProvider(StockEntities dbContext)
        {
            DbContext = dbContext;
        }

        public Price GetPrice(string name, DateTime dateTime)
        {
            var query = from price in DbContext.Prices
                        join company in DbContext.Companies on price.CompanyId equals company.Id
                        where company.Name == name
                        && price.DateTime == dateTime
                        select price;
            return query.SingleOrDefault();
        }

        public void AddPrice(Price price)
        {
            DbContext.Prices.Add(price);
        }

        public List<Price> GetPriceListByDay(string name, DateTime dateTime)
        {
            var query = from price in DbContext.Prices
                        join company in DbContext.Companies on price.CompanyId equals company.Id
                        where company.Name == name
                        && price.DateTime.Year == dateTime.Year
                        && price.DateTime.Month == dateTime.Month
                        && price.DateTime.Day == dateTime.Day
                        select price;
            return query.ToList();
        }

        public void AddCompany(Company company)
        {
            DbContext.Companies.Add(company);
        }

        public Company GetCompany(string name)
        {
            return DbContext.Companies.Where(x => x.Name == name).SingleOrDefault();
        }

        public void Save()
        {
            DbContext.SaveChanges();
        }
    }
}
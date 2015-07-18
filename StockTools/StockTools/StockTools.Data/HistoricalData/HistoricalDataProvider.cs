using System;
using System.Collections.Generic;
using System.Linq;

namespace StockTools.Data.HistoricalData
{
    public class HistoricalDataProvider : IHistoricalDataProvider
    {
        public HistoricalDataContainer DbContext { get; set; }

        public HistoricalDataProvider(HistoricalDataContainer dbContext)
        {
            DbContext = dbContext;
        }

        Price IHistoricalDataProvider.GetSingle(string name, DateTime dateTime)
        {
            var query = from price in DbContext.Prices
                        where price.Name == name
                        && price.DateTime == dateTime
                        select price;
            return query.SingleOrDefault();
        }

        public void AddSingle(Price price)
        {
            DbContext.Prices.Add(price);
        }

        List<Price> IHistoricalDataProvider.GetListByDay(string name, DateTime dateTime)
        {
            var query = from price in DbContext.Prices
                        where price.Name == name
                        && price.DateTime.Year == dateTime.Year
                        && price.DateTime.Month == dateTime.Month
                        && price.DateTime.Day == dateTime.Day
                        select price;
            return query.ToList();
        }
    }
}
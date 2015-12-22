using StockTools.Core.Interfaces;
using StockTools.Data.HistoricalData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockTools.Infrastructure.Data
{
    public class DbHistoricalPriceRepository : IHistoricalPriceRepository
    {
        #region DI

        private IDbHistoricalDataProvider _dbHistoricalDataProvider;

        public IDbHistoricalDataProvider DbHistoricalDataProvider
        {
            get { return _dbHistoricalDataProvider; }
            set { _dbHistoricalDataProvider = value; }
        }

        #endregion DI

        public DbHistoricalPriceRepository(IDbHistoricalDataProvider dbHistoricalDataProvider)
        {
            _dbHistoricalDataProvider = dbHistoricalDataProvider;
        }

        public double Get(string companyName, DateTime dateTime)
        {
            return _dbHistoricalDataProvider.GetPrice(companyName, dateTime).Close;
        }

        public double GetClosest(string companyName, DateTime dateTime)
        {
            var pricesByDay = this.GetAll(companyName, dateTime);

            DateTime closestDate = pricesByDay.Keys
                                              .OrderBy(t => Math.Abs((t - dateTime).Ticks))
                                              .First();
            return pricesByDay[closestDate];
        }

        public Dictionary<DateTime, double> GetAll(string companyName, DateTime day)
        {
            var result = new Dictionary<DateTime, double>();
            var prices = _dbHistoricalDataProvider.GetPriceListByDay(companyName, day);

            foreach (var price in prices)
            {
                result[price.DateTime] = price.Close;
            }

            return result;
        }

        public bool AnyTradingInDay(DateTime dateTime)
        {
            return _dbHistoricalDataProvider.AnyTradingInDay(dateTime);
        }

        public bool IsThereCompany(string companyName, DateTime dateTime)
        {
            return _dbHistoricalDataProvider.IsThereCompany(companyName, dateTime);
        }
    }
}
using StockTools.Data.HistoricalData;
using StockTools.Domain.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StockTools.BusinessLogic.Concrete
{
    public class PriceProvider : IPriceProvider
    {
        #region DI

        public IDbHistoricalDataProvider DbHistoricalDataProvider { get; set; }

        public PriceProvider(IDbHistoricalDataProvider dbHistoricalDataProvider)
        {
            DbHistoricalDataProvider = dbHistoricalDataProvider;
        }

        #endregion DI

        public double GetPrice(string companyName)
        {
            throw new NotImplementedException();
        }

        public double GetPrice(string companyName, DateTime dateTime)
        {
            return DbHistoricalDataProvider.GetPrice(companyName, dateTime).Close;
        }

        public double GetClosestPrice(string companyName, DateTime dateTime)
        {
            var pricesByDay = this.GetPrices(companyName, dateTime);

            DateTime closestDate = pricesByDay.Keys
                                              .OrderBy(t => Math.Abs((t - dateTime).Ticks))
                                              .First();
            return pricesByDay[closestDate];
        }

        public Dictionary<DateTime, double> GetPrices(string companyName, DateTime day)
        {
            var prices = DbHistoricalDataProvider.GetPriceListByDay(companyName, day);
            var result = new Dictionary<DateTime, double>();
            prices.ForEach(x => result[x.DateTime] = x.Close);
            return result;
        }
    }
}
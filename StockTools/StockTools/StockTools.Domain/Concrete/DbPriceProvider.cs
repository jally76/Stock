using StockTools.Data.HistoricalData;
using StockTools.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.BusinessLogic.Concrete
{
    public class DbPriceProvider : IPriceProvider
    {
        #region DI

        public IHistoricalDataProvider HistoricalDataProvider { get; set; }

        public DbPriceProvider(IHistoricalDataProvider historicalDataProvider)
        {
            HistoricalDataProvider = historicalDataProvider;
        }

        #endregion

        public double GetPrice(string companyName)
        {
            throw new NotImplementedException();
        }

        public double GetPrice(string companyName, DateTime dateTime)
        {
            return HistoricalDataProvider.GetPrice(companyName, dateTime).Close;
        }

        public Dictionary<DateTime, double> GetPrices(string companyName, DateTime day)
        {
            var prices = HistoricalDataProvider.GetPriceListByDay(companyName, day);
            var result = new Dictionary<DateTime, double>();
            prices.ForEach(x => result[x.DateTime] = x.Close);
            return result;
        }
    }
}

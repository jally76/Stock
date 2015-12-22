using StockTools.Core.Interfaces;
using StockTools.Core.Models;
using System;
using System.Threading.Tasks;

namespace StockTools.Core.Services
{
    public class StockSystemSimulator : IStockSystemSimulator
    {
        private IHistoricalPriceRepository _historicalPriceRepository;

        public IHistoricalPriceRepository HistoricalPriceRepository
        {
            get { return _historicalPriceRepository; }
            set { _historicalPriceRepository = value; }
        }

        public StockSystemSimulator(DateTime beginDate, IHistoricalPriceRepository historicalPriceRepository)
        {
            _historicalPriceRepository = historicalPriceRepository;
        }

        public DateTime CurrentDate
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsStockMarketAvailable
        {
            get { throw new NotImplementedException(); }
        }

        public async Task<Transaction> SubmitOrder(Models.Order order)
        {
            throw new NotImplementedException();
        }

        public void Tick(TimeSpan timeSpan)
        {
            throw new NotImplementedException();
        }
    }
}
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
        }

        public StockSystemSimulator(DateTime beginDate, IHistoricalPriceRepository historicalPriceRepository)
        {
            _historicalPriceRepository = historicalPriceRepository;
            _currentDate = beginDate;
        }

        private DateTime _currentDate;

        public DateTime CurrentDate
        {
            get { return _currentDate; }
        }

        public bool IsStockMarketAvailable
        {
            get { return _historicalPriceRepository.AnyTradingInDay(_currentDate); }
        }

        public async Task<Transaction> SubmitOrder(Models.Order order)
        {
            throw new NotImplementedException();
        }

        public void Tick(TimeSpan timeSpan)
        {
            _currentDate = _currentDate.Add(timeSpan);
            //TODO
        }
    }
}
using StockTools.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace StockTools.Core.Services
{
    public class StrategyTestRunner : IStrategyTestRunner
    {
        public IHIstoricalPriceRepository HistoricalPriceRepository { get; set; }

        public StrategyTestRunner(IHIstoricalPriceRepository historicalPriceRepository)
        {
            HistoricalPriceRepository = historicalPriceRepository;
        }

        public Dictionary<DateTime, double> Run(IStrategy strategy, IPortfolio portfolio, DateTime from, DateTime to, TimeSpan interval)
        {
            var result = new Dictionary<DateTime, double>();
            var date = from;
            while (date < to)
            {
                //TODO
                date.Add(interval);
            }
            return result;
        }
    }
}
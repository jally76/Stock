using System;
using System.Collections.Generic;

namespace StockTools.Core.Interfaces
{
    public interface IStrategyTestRunner
    {
        public IHIstoricalPriceRepository HistoricalPriceRepository { get; set; }

        /// <summary>
        /// Runs strategy within a time over a portfolio
        /// </summary>
        /// <param name="strategy">Strategy</param>
        /// <param name="portfolio">Portfolio</param>
        /// <param name="from">Begin date</param>
        /// <param name="to">End date</param>
        /// <returns></returns>
        Dictionary<DateTime, double> Run(IStrategy strategy, IPortfolio portfolio, DateTime from, DateTime to, TimeSpan interval);
    }
}
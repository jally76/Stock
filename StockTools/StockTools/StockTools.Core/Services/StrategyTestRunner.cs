using StockTools.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace StockTools.Core.Services
{
    public class StrategyTestRunner : IStrategyTestRunner
    {
        #region DI

        private IHistoricalPriceRepository _historicalPriceRepository;

        public IHistoricalPriceRepository HistoricalPriceRepository
        {
            get { return _historicalPriceRepository; }
            set { _historicalPriceRepository = value; }
        }

        private IPortfolio _portfolio;

        public IPortfolio Portfolio
        {
            get { return _portfolio; }
            set { _portfolio = value; }
        }

        private IStockSystemSimulator _stockSystemSimulator;

        public IStockSystemSimulator StockSystemSimulator
        {
            get { return _stockSystemSimulator; }
            set { _stockSystemSimulator = value; }
        }

        public StrategyTestRunner(IHistoricalPriceRepository historicalPriceRepository, IPortfolio portfolio, IStockSystemSimulator stockSystemSimulator)
        {
            _historicalPriceRepository = historicalPriceRepository;
            _portfolio = portfolio;
            _stockSystemSimulator = stockSystemSimulator;
        }

        #endregion DI

        public Dictionary<DateTime, double> Run(IStrategy strategy, DateTime from, DateTime to, TimeSpan interval)
        {
            var result = new Dictionary<DateTime, double>();
            var date = from;
            while (date < to)
            {
                var orders = strategy.GenerateOrders();
                foreach (var order in orders)
                {
                    var transaction = _stockSystemSimulator.SubmitOrder(order);
                    _portfolio.AddTransaction(transaction);
                }

                date.Add(interval);
            }
            return result;
        }
    }
}
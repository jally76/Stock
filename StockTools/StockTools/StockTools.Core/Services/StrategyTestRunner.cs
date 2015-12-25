using StockTools.Core.Interfaces;
using StockTools.Core.Models.EventArgs;
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

        private IProfitCalculator _profitCalculator;

        public IProfitCalculator ProfitCalculator
        {
            get { return _profitCalculator; }
            set { _profitCalculator = value; }
        }

        public StrategyTestRunner(IHistoricalPriceRepository historicalPriceRepository, IPortfolio portfolio, IStockSystemSimulator stockSystemSimulator, IProfitCalculator profitCalculator)
        {
            _historicalPriceRepository = historicalPriceRepository;
            _portfolio = portfolio;
            _stockSystemSimulator = stockSystemSimulator;
            _profitCalculator = profitCalculator;
        }

        #endregion DI

        public Dictionary<DateTime, double> Run(IStrategy strategy, DateTime from, DateTime to, TimeSpan interval)
        {
            var result = new Dictionary<DateTime, double>();
            var date = from;
            _stockSystemSimulator.OrderProcessed += this.OrderProcessed;
            while (date <= to)
            {
                var orders = strategy.GenerateOrders();
                foreach (var order in orders)
                {
                    _stockSystemSimulator.SubmitOrder(order);
                }

                result[date] = _profitCalculator.GetGrossProfit(_portfolio.Transactions, _portfolio.Dividends, _portfolio.Items, date);

                date = date.Add(interval);
                _stockSystemSimulator.Tick(interval);
            }
            return result;
        }

        private void OrderProcessed(object sender, OrderEventArgs e)
        {
            _portfolio.AddTransaction(e.Transaction);
        }
    }
}
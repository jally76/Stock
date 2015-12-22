using StockTools.Core.Interfaces;
using StockTools.Core.Models;
using System;

namespace StockTools.Core.Services
{
    public class OrderProcessor : IOrderProcessor
    {
        private IStockSystemSimulator _stockSystemSimulator;

        public IStockSystemSimulator StockSystemSimulator
        {
            get { return _stockSystemSimulator; }
        }

        private IPortfolio _portfolio;

        public IPortfolio Portfolio
        {
            get { return _portfolio; }
            set { _portfolio = value; }
        }


        public OrderProcessor(IStockSystemSimulator stockSystemSimulator,
                              IPortfolio portfolio)
        {
            _stockSystemSimulator = stockSystemSimulator;
            _portfolio = portfolio;
        }

        public Transaction ProcessOrder(Order order)
        {
            var result = new Transaction();

            //result.

            return result;
        }
    }
}
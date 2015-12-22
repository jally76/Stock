using StockTools.Core.Interfaces;
using StockTools.Core.Models;
using StockTools.Core.Models.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockTools.Core.Services
{
    public class StockSystemSimulator : IStockSystemSimulator
    {
        #region DI

        private IHistoricalPriceRepository _historicalPriceRepository;

        public IHistoricalPriceRepository HistoricalPriceRepository
        {
            get { return _historicalPriceRepository; }
        }

        private IOrderProcessor _orderProcessor;

        public IOrderProcessor OrderProcessor
        {
            get { return _orderProcessor; }
            set { _orderProcessor = value; }
        }

        public StockSystemSimulator(DateTime beginDate, IHistoricalPriceRepository historicalPriceRepository, IOrderProcessor orderProcessor)
        {
            _historicalPriceRepository = historicalPriceRepository;
            _orderProcessor = orderProcessor;
            _currentDate = beginDate;
        }

        #endregion DI

        private DateTime _currentDate;

        public DateTime CurrentDate
        {
            get { return _currentDate; }
        }

        public bool IsStockMarketAvailable
        {
            get { return _historicalPriceRepository.AnyTradingInDay(_currentDate); }
        }

        private Queue<Order> _orderQueue;

        public void Tick(TimeSpan timeSpan)
        {
            _currentDate = _currentDate.Add(timeSpan);
            //TODO
        }

        public Transaction SubmitOrder(Order order, IPortfolio portfolio)
        {
            //1. Is there stock like this?
            //2. What's the price of stock?
            //3. Is there enough money?
            //4. Transform order to transaction
            if (_historicalPriceRepository.IsThereCompany(order.CompanyName, CurrentDate) == false)
            {
                var ex = new Exception("There's no company with given name on the market");
                ex.Data["CompanyName"] = order.CompanyName;
                throw ex;
            }
            var stockPrice = _historicalPriceRepository.GetClosest(order.CompanyName, CurrentDate);
            var orderValue = order.Amount * stockPrice;
            if (portfolio.Cash < orderValue)
            {
                throw new NotEnoughMoneyException(portfolio);
            }

            return _orderProcessor.ProcessOrder(order, portfolio);
        }
    }
}
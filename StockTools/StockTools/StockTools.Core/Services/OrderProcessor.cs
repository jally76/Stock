using StockTools.Core.Interfaces;
using StockTools.Core.Models;
using StockTools.Core.Models.Delegates;
using StockTools.Core.Models.EventArgs;
using StockTools.Core.Models.Exceptions;
using System;
using System.Collections.Generic;

namespace StockTools.Core.Services
{
    public class OrderProcessor : IOrderProcessor
    {
        #region DI

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

        private IHistoricalPriceRepository _historicalPriceRepository;

        public IHistoricalPriceRepository HistoricalPriceRepository
        {
            get { return _historicalPriceRepository; }
        }

        public OrderProcessor(IStockSystemSimulator stockSystemSimulator,
                              IPortfolio portfolio,
                              IHistoricalPriceRepository historicalPriceRepository)
        {
            _stockSystemSimulator = stockSystemSimulator;
            _portfolio = portfolio;
            _historicalPriceRepository = historicalPriceRepository;
            _ordersToBeProcessed = new List<Order>();
        }

        #endregion DI

        private bool IsOrderValid(Order order)
        {
            //TODO
            return true;
        }

        private List<Order> _ordersToBeProcessed;

        public void OnCurrentDateChanged(object sender, EventArgs e)
        {
            var ordersToBeRemoved = new List<Order>();

            foreach (var order in _ordersToBeProcessed)
            {
                //TODO Here should be some logic for processing order
                //and some GREAT VALIDATOR ;-)
                OrderProcessed(this, new OrderEventArgs(new Transaction
                {
                    Amount = order.Amount,
                    //TODO CompanyId = 
                    //TODO Implement also current date feature in the future
                    Price = _historicalPriceRepository.GetClosest(order.CompanyName, _stockSystemSimulator.CurrentDate),
                    CompanyName = order.CompanyName,
                    //TODO Price = 
                    Time = _stockSystemSimulator.CurrentDate,
                    TransactionType = order.OrderType == Order.OrderTypes.Buy ? Transaction.TransactionTypes.Buy : Transaction.TransactionTypes.Sell
                }));
                ordersToBeRemoved.Add(order);
            }

            foreach (var order in ordersToBeRemoved)
            {
                _ordersToBeProcessed.Remove(order);
            }
        }

        public void Enqueue(Order order)
        {
            if (IsOrderValid(order))
            {
                _ordersToBeProcessed.Add(order);
            }
            else
            {
                throw new InvalidOrderException();
            }
        }

        [Obsolete]
        public Transaction ProcessOrder(Order order)
        {
            if (IsOrderValid(order))
            {
                return new Transaction
                {
                    Amount = order.Amount,
                    //TODO CompanyId = 
                    //TODO Implement also current date feature in the future
                    Price = _historicalPriceRepository.GetClosest(order.CompanyName, _stockSystemSimulator.CurrentDate),
                    CompanyName = order.CompanyName,
                    //TODO Price = 
                    Time = _stockSystemSimulator.CurrentDate,
                    TransactionType = order.OrderType == Order.OrderTypes.Buy ? Transaction.TransactionTypes.Buy : Transaction.TransactionTypes.Sell
                };
            }
            else
            {
                throw new InvalidOrderException();
            }
        }

        public event OrderProcessedDelegate OrderProcessed;
    }
}
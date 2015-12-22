using StockTools.Core.Interfaces;
using StockTools.Core.Models;
using StockTools.Core.Models.Exceptions;

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
        }

        #endregion DI

        private bool IsOrderValid(Order order)
        {
            //TODO
            return true;
        }

        public Transaction ProcessOrder(Order order)
        {
            if (IsOrderValid(order))
            {
                return new Transaction
                {
                    Amount = order.Amount,
                    //TODO CompanyId = 
                    //TODO Implement also current date feature in the future
                    Price = _historicalPriceRepository.GetClosest(order.CompanyName,_stockSystemSimulator.CurrentDate),
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
    }
}
using StockTools.Domain.Abstract;
using StockTools.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.Domain.Concrete
{
    public class TestRunner : ITestRunner
    {
        public double RunStrategy(IStrategy strategy, IPortfolio portfolio, IPriceProvider priceProvider, DateTime startDate, DateTime endDate, long interval)
        {
            var now = startDate;
            do
            {
                var orders = strategy.GenerateOrders(priceProvider, portfolio, now);
                if (orders != null && orders.Count > 0)
                {
                    foreach (var order in orders)
                    {
                        var currentPrice = priceProvider.GetClosestPrice(order.CompanyName, now);
                        if (CheckConditions(portfolio, currentPrice, now, order))
                        {
                            portfolio.AddTransaction(PrepareTransaction(order, currentPrice, now));
                        }
                    }
                }
                now = now.AddSeconds(interval);
            } while (now <= endDate);
            return portfolio.Cash;
        }

        private static Transaction PrepareTransaction(Order order, double currentPrice, DateTime now)
        {
            return new Transaction()
            {
                Amount = order.Amount,
                CompanyName = order.CompanyName,
                Price = currentPrice,
                Time = now,
                TransactionType = order.OrderType.ToString() == "Buy" ? Transaction.TransactionTypes.Buy : Transaction.TransactionTypes.Sell
            };
        }

        private static bool CheckConditions(IPortfolio portfolio, double currentPrice, DateTime now, Order order)
        {
            //Check more conditions
            var orderValue = currentPrice * order.Amount + portfolio.ChargeFunction(currentPrice * order.Amount);

            var isPriceBelowLimit = order.AnyPrice ? true : currentPrice < order.PriceLimit;
            var isPriceAboveLimit = order.AnyPrice ? true : currentPrice > order.PriceLimit;
            var isEnoughCash = order.OrderType == Order.OrderTypes.Sell ? true : portfolio.Cash > orderValue + portfolio.ChargeFunction(orderValue);

            if (order.OrderType == Order.OrderTypes.Buy)
            {
                if (isPriceBelowLimit && isEnoughCash)
                {
                    return true;
                }
            }
            if (order.OrderType == Order.OrderTypes.Sell)
            {
                if (isPriceAboveLimit)
                {
                    return true;
                }
            }

            return false;
        }

        public IStrategy FindBestStrategy(List<IStrategy> strategies, IPortfolio portfolio, IPriceProvider priceProvider, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}

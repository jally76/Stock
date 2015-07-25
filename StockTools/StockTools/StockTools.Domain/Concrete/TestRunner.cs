using StockTools.Domain.Abstract;
using StockTools.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.Domain.Concrete
{
    public class BasicTestRunner : ITestRunner
    {
        public double RunStrategy(IStrategy strategy, IPortfolio portfolio, IPriceProvider priceProvider, DateTime startDate, DateTime endDate, long interval)
        {
            var now = startDate;
            do
            {
                var orders = strategy.GenerateOrders(priceProvider, portfolio, now);
                if (orders != null)
                {
                    foreach (var order in orders)
                    {
                        var currentPrice = priceProvider.GetPrice(order.CompanyName, now);
                        if (CheckConditions(portfolio, currentPrice, now, order))
                        {
                            portfolio.AddTransaction(PrepareTransaction(order, currentPrice, now));
                        }
                    }
                }
                now = now.AddMinutes(5);
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

        private static bool CheckConditions(IPortfolio portfolio, double? currentPrice, DateTime now, Order order)
        {
            if (!currentPrice.HasValue)
            {
                return false;
            }

            //Check more conditions
            var orderValue = currentPrice * order.Amount + portfolio.ChargeFunction(currentPrice.Value * order.Amount);

            var isPriceBelowLimit = currentPrice < order.PriceLimit;
            var isPriceAboveLimit = currentPrice > order.PriceLimit;
            var isEnoughCash = portfolio.Cash > orderValue;

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

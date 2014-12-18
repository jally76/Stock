using StockTools.BusinessLogic.Abstract;
using StockTools.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.BusinessLogic.Concrete
{
    public class BasicTestRunner : ITestRunner
    {
        public double RunStrategy(IStrategy strategy, IPortfolio portfolio, IArchivePriceProvider priceProvider, DateTime startDate, DateTime endDate)
        {
            var now = startDate;
            do
            {
                var orders = strategy.GenerateOrders(priceProvider, portfolio, now);
                if (orders != null)
                {
                    foreach (var order in orders)
                    {
                        //TODO Prepare order - check more prerequisites
                        var currentPrice = priceProvider.GetPriceByFullNameAndDateTime(order.CompanyName, now);
                        var orderValue = currentPrice * order.Amount + portfolio.ChargeFunction(currentPrice * order.Amount);

                        var isPriceBelowLimit = currentPrice < order.PriceLimit;
                        var isPriceAboveLimit = currentPrice > order.PriceLimit;
                        var isEnoughCash = portfolio.Cash > orderValue;

                        var transaction = new Transaction()
                        {
                            Amount = order.Amount,
                            CompanyName = order.CompanyName,
                            Price = currentPrice,
                            Time = now,
                            TransactionType = order.OrderType.ToString() == "Buy" ? Transaction.TransactionTypes.Buy : Transaction.TransactionTypes.Sell
                        };

                        if (transaction.TransactionType == Transaction.TransactionTypes.Buy)
                        {
                            if (isPriceBelowLimit && isEnoughCash)
                            {
                                portfolio.AddTransaction(transaction);
                            }
                        }
                        if (transaction.TransactionType == Transaction.TransactionTypes.Sell)
                        {
                            if (isPriceAboveLimit)
                            {
                                portfolio.AddTransaction(transaction);
                            }
                        }
                    }
                }
                //TODO Transform orders into transactions
                now = now.AddMinutes(15);
            } while (now <= endDate);
            return portfolio.Cash;
        }

        public IStrategy FindBestStrategy(List<IStrategy> strategies, IPortfolio portfolio, IArchivePriceProvider priceProvider, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}

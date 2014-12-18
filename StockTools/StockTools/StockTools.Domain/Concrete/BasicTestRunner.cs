using StockTools.BusinessLogic.Abstract;
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
                var transactions = strategy.GenerateOrders(priceProvider, portfolio, now);
                //portfolio.Transactions.AddRange(transactions);
                //TODO Transform orders into transactions
                now = now.AddMinutes(5);
            } while (now >= endDate);
            return portfolio.Value + portfolio.Cash;
        }

        public IStrategy FindBestStrategy(List<IStrategy> strategies, IPortfolio portfolio, IArchivePriceProvider priceProvider, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}

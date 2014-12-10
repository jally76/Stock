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
        public double RunStrategy(IStrategy strategy, IPortfolio portfolio, IPriceProvider priceProvider, DateTime startDate, DateTime endDate)
        {
            var now = startDate;
            do
            {
                var transactions = strategy.GenerateTransactions(priceProvider, portfolio);
                portfolio.Transactions.AddRange(transactions);
            } while (now >= endDate);
            return portfolio.Value + portfolio.Cash;
        }

        public IStrategy FindBestStrategy(List<IStrategy> strategies, IPortfolio portfolio, IPriceProvider priceProvider, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}

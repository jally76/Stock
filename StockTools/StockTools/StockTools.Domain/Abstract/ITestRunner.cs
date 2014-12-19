using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.Domain.Abstract
{
    public interface ITestRunner
    {
        double RunStrategy(IStrategy strategy, IPortfolio portfolio, IArchivePriceProvider priceProvider, DateTime startDate, DateTime endDate);
        IStrategy FindBestStrategy(List<IStrategy> strategies, IPortfolio portfolio, IArchivePriceProvider priceProvider, DateTime startDate, DateTime endDate);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.Domain.Abstract
{
    public interface ITestRunner
    {
        /// <summary>
        /// Calculates net profit of running particular strategy in given period of time with given interval.
        /// </summary>
        /// <param name="strategy">Strategy object</param>
        /// <param name="portfolio">Portfolio object</param>
        /// <param name="priceProvider">Price provider object</param>
        /// <param name="startDate">Date from which simulation will start</param>
        /// <param name="endDate">End date of simulation process</param>
        /// <param name="interval">Interval between running strategy's method to generate new orders based on prices on the market. 
        /// Value is in seconds. If it's zero then interval will be single tick (simulation as 'dense' as possible).
        /// </param>
        /// <returns>Sum of realised profit and value of stocks at the end date.</returns>
        double RunStrategy(IStrategy strategy, IPortfolio portfolio, IPriceProvider priceProvider, DateTime startDate, DateTime endDate, long interval);

        IStrategy FindBestStrategy(List<IStrategy> strategies, IPortfolio portfolio, IPriceProvider priceProvider, DateTime startDate, DateTime endDate);
    }
}

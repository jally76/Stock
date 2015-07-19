using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.Domain.Abstract
{
    public interface IPriceProvider
    {
        /// <summary>
        /// Gets current price
        /// </summary>
        /// <param name="companyName">Full company name (for instance CIECH)</param>
        /// <returns></returns>
        double GetPrice(string companyName);

        /// <summary>
        /// Gets historical price
        /// </summary>
        /// <param name="companyName">Full company name (for instance CIECH)</param>
        /// <param name="dateTime">Date time</param>
        /// <returns></returns>
        double GetPrice(string companyName, DateTime dateTime);

        /// <summary>
        /// Gets list of prices with timestamps in a given day
        /// </summary>
        /// <param name="companyName">Full company name (for instance CIECH)</param>
        /// <param name="day">Year month and day</param>
        /// <returns></returns>
        Dictionary<DateTime, double> GetPrices(string companyName, DateTime day);
    }
}

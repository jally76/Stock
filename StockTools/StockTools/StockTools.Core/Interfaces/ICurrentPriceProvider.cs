using System;
using System.Collections.Generic;

namespace StockTools.Core.Interfaces
{
    public interface ICurrentPriceProvider
    {
        /// <summary>
        /// Gets current price
        /// </summary>
        /// <param name="companyName">Full company name (for instance CIECH)</param>
        /// <returns></returns>
        double Get(string companyName);
    }
}
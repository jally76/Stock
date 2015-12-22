using StockTools.Core.Models;
using System;
using System.Collections.Generic;

namespace StockTools.Core.Interfaces
{
    public interface IProfitCalculator
    {
        #region Dependencies

        /// <summary>
        /// Price service
        /// </summary>
        ICurrentPriceProvider CurrentPriceProvider { get; set; }

        /// <summary>
        /// Price service
        /// </summary>
        IHIstoricalPriceRepository HistoricalPriceRepository { get; set; }

        #endregion

        /// <summary>
        /// Gross profit of the portfolio
        /// </summary>
        double GetGrossProfit(List<Transaction> transactions,
                              List<Dividend> dividends,
                              List<InvestmentPortfolioItem> items);

        /// <summary>
        /// Net profit of the portfolio
        /// </summary>
        double GetNetProfit(List<Transaction> transactions,
                            List<Dividend> dividends,
                            List<InvestmentPortfolioItem> items,
                            Func<double, double> chargeFunction,
                            List<Taxation> taxationList);

        /// <summary>
        /// Gross realised profit of the portfolio (only sold shares)
        /// </summary>
        double GetRealisedGrossProfit(List<Transaction> transactions,
                                      List<Dividend> dividends,
                                      List<InvestmentPortfolioItem> items);

        /// <summary>
        /// Gross realised profit of the portfolio (only sold shares) until specified date
        /// </summary>
        double GetRealisedGrossProfit(List<Transaction> transactions,
                                      List<Dividend> dividends,
                                      List<InvestmentPortfolioItem> items,
                                      DateTime? date);

        /// <summary>
        /// Net realised profit of the portfolio (only sold shares)
        /// </summary>
        double GetRealisedNetProfit(List<Transaction> transactions,
                                    List<Dividend> dividends,
                                    List<InvestmentPortfolioItem> items);
    }
}
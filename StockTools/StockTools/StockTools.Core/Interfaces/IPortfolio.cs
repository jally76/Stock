using StockTools.Core.Models;
using System;
using System.Collections.Generic;

namespace StockTools.Core.Interfaces
{
    public interface IPortfolio
    {
        #region Fields

        /// <summary>
        /// Cash which is not invested
        /// </summary>
        double Cash { get; }

        /// <summary>
        /// List of items in the portfolio
        /// </summary>
        List<InvestmentPortfolioItem> Items { get; }

        /// <summary>
        /// List of transactions
        /// </summary>
        List<Transaction> Transactions { get; }

        /// <summary>
        /// List of dividends
        /// </summary>
        List<Dividend> Dividends { get; set; }

        /// <summary>
        /// Sets charge function which is necessary for profit calculation
        /// </summary>
        Func<double, double> ChargeFunction { get; set; }

        /// <summary>
        /// Sets value of capital gain tax (during the time because we assume that it can change)
        /// </summary>
        List<Taxation> TaxationList { set; }

        #endregion Fields

        #region Methods

        /// <summary>
        /// Adds tranaction to portfolio
        /// </summary>
        /// <param name="transaction"></param>
        void AddTransaction(Transaction transaction);

        #endregion Methods
    }
}
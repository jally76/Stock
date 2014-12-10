using StockTools.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.BusinessLogic.Abstract
{
    public interface IPortfolio
    {
        /// <summary>
        /// Handle to price service
        /// </summary>
        IPriceProvider PriceService { get; set; }

        /// <summary>
        /// List of items in the portfolio
        /// </summary>
        List<InvestmentPortfolioItem> Items { get; set; }

        /// <summary>
        /// List of transactions
        /// </summary>
        List<MBankTransaction> Transactions { get; set; }

        /// <summary>
        /// Sets charge function which is necessary for profit calculation
        /// </summary>
        Func<double, double> ChargeFunction { get; set; }

        /// <summary>
        /// Sum of current prices of all shares in the portfolio
        /// </summary>
        double GetValue();

        /// <summary>
        /// Gross profit of the portfolio
        /// </summary>
        double GetGrossProfit();

        /// <summary>
        /// Net profit of the portfolio
        /// </summary>
        double GetNetProfit();

        /// <summary>
        /// Gross realised profit of the portfolio (only sold shares)
        /// </summary>
        double GetRealisedGrossProfit();

        /// <summary>
        /// Net realised profit of the portfolio (only sold shares)
        /// </summary>
        double GetRealisedNetProfit();

        /// <summary>
        /// Sets value of capital gain tax (during the time because we assume that it can change)
        /// </summary>
        void SetTaxation(List<Taxation> taxationList);

        /// <summary>
        /// Gets list of transactions which can be paired (buy, sell)
        /// </summary>
        /// <returns></returns>
        List<MBankTransaction> GetPairedTransactions();

        /// <summary>
        /// Gets pair of transaction (finds later transactions, so an argument has to be the first one)
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        MBankTransaction GetTransactionPair(MBankTransaction transaction);
    }
}

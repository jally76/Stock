using StockTools.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.BusinessLogic.Abstract
{
    public interface IBookkeepingService
    {
        /// <summary>
        /// Reads CSV with transaction history and builds list of transaction objects
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        List<Transaction> ReadTransactionHistory(MemoryStream stream);

        /// <summary>
        /// Builds investment portfolio from full transaction list
        /// </summary>
        /// <param name="transactions"></param>
        /// <returns></returns>
        InvestmentPortfolio BuildInvestmentPortfolio(List<Transaction> transactions);
    }
}

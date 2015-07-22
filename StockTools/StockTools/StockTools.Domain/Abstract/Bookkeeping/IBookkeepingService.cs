using StockTools.Domain.Concrete;
using StockTools.Entities;
using System.Collections.Generic;
using System.IO;

namespace StockTools.Domain.Abstract
{
    public interface IBookkeepingService
    {
        /// <summary>
        /// Reads CSV with transaction history and builds list of transaction objects
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        List<Transaction> ReadTransactionHistory(MemoryStream stream);
    }
}

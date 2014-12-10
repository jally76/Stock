using StockTools.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.BusinessLogic.Abstract
{
    public interface IStrategy
    {
        List<Transaction> GenerateTransactions(IPriceProvider priceProvider, IPortfolio portfolio);
    }
}

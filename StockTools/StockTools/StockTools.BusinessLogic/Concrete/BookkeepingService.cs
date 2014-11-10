using StockTools.BusinessLogic.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.BusinessLogic.Concrete
{
    public class BookkeepingService : IBookkeepingService
    {
        public List<Entities.Transaction> ReadTransactionHistory(System.IO.MemoryStream stream)
        {
            throw new NotImplementedException();
        }

        public Entities.InvestmentPortfolio BuildInvestmentPortfolio(List<Entities.Transaction> transactions)
        {
            throw new NotImplementedException();
        }
    }
}

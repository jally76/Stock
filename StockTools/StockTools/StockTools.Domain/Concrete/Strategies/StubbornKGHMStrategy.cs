using StockTools.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.BusinessLogic.Concrete.Strategies
{
    public class StubbornKGHMStrategy : IStrategy
    {
        public List<Entities.Order> GenerateOrders(IArchivePriceProvider priceProvider, IPortfolio portfolio, DateTime dateTime)
        {
            ////If price is lower over 2% than buy price then sell and wait 3 days, if price is higher over 5% than buy price then sell and wait 3 days.

            //var kghm = portfolio.Items.Where(x=>x.CompanyName == "KGHM").SingleOrDefault();
            //var isBought = kghm != null && kghm.NumberOfShares > 0;

            //var lastKghmTransaction = portfolio.Transactions.Where(x => x.CompanyName == "KGHM").OrderBy(x => x.Time).FirstOrDefault();

            throw new NotImplementedException();
        }
    }
}

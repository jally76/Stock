using StockTools.BusinessLogic.Abstract;
using StockTools.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.BusinessLogic.Concrete.Strategies
{
    public class TestStrategy : IStrategy
    {
        public List<Order> GenerateOrders(IArchivePriceProvider priceProvider, IPortfolio portfolio, DateTime dateTime)
        {
            throw new NotImplementedException();
        }
    }
}

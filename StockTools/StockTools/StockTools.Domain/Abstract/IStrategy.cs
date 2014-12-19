using StockTools.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.Domain.Abstract
{
    public interface IStrategy
    {
        List<Order> GenerateOrders(IArchivePriceProvider priceProvider, IPortfolio portfolio, DateTime dateTime);
    }
}

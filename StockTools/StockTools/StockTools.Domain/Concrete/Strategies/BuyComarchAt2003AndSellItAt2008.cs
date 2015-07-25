using StockTools.Domain.Abstract;
using StockTools.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.Domain.Concrete.Strategies
{
    public class BuyComarchAt2003AndSellItAt2008 : IStrategy
    {
        public List<Order> GenerateOrders(IPriceProvider priceProvider, IPortfolio portfolio, DateTime dateTime)
        {
            var result = new List<Order>();
            if (dateTime.Year == 2003 && dateTime.Month == 2 && dateTime.Day == 3
                && dateTime.Hour == 10 && dateTime.Minute == 0 && dateTime.Second == 3)
            {
                result.Add(new Order()
                {
                    AnyPrice = true,
                    Amount = 10,
                    CompanyName = "COMARCH",
                    OrderType = Order.OrderTypes.Buy
                });
            }

            if (dateTime.Year == 2008 && dateTime.Month == 2 && dateTime.Day == 1
                && dateTime.Hour == 9 && dateTime.Minute == 30 && dateTime.Second == 10)
            {
                result.Add(new Order()
                {
                    AnyPrice = true,
                    Amount = 10,
                    CompanyName = "COMARCH",
                    OrderType = Order.OrderTypes.Sell
                });
            }

            return result;
        }
    }
}

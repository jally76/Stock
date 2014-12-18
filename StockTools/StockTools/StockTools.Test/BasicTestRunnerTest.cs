using Moq;
using StockTools.BusinessLogic.Abstract;
using StockTools.BusinessLogic.Concrete;
using StockTools.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StockTools.Test
{
    public class BasicTestRunnerTest
    {
        private double ChargeFunc(double price)
        {
            if (price <= 769)
            {
                return 3.0;
            }
            else
            {
                return price * (0.39 / 100.0);
            }
        }

        [Fact]
        public void BasicTestRunnerTest_BasicPortfolio_BOSSAArchivePriceProvider()
        {
            #region Arrange

            ITestRunner runner = new BasicTestRunner();

            var dateFrom = new DateTime(2001, 1, 2, 9, 0, 0);
            var dateTo = new DateTime(2004, 1, 2, 9, 0, 0);

            var path = Environment.CurrentDirectory + "\\Files\\BOSSA\\Intraday\\";
            IArchivePriceProvider priceProvider = new BOSSAArchivePriceProvider(path);

            Mock<ICurrentPriceProvider> currentPriceProviderMock = new Mock<ICurrentPriceProvider>();
            currentPriceProviderMock.Setup(x => x.GetPriceByFullName(It.IsAny<string>())).Returns(1.0);
            currentPriceProviderMock.Setup(x => x.GetPriceByShortName(It.IsAny<string>())).Returns(1.0);

            Mock<IStrategy> orderMock = new Mock<IStrategy>();
            var ordersBuy = new List<Order>();
            ordersBuy.Add(new Order()
            {
                CompanyName = "AMICA",
                PriceLimit = 1000,
                Amount = 100,
                OrderType = Order.OrderTypes.Buy
            });
            ordersBuy.Add(new Order()
            {
                CompanyName = "KGHM",
                PriceLimit = 1000,
                Amount = 100,
                OrderType = Order.OrderTypes.Buy
            });

            var ordersSell = new List<Order>();
            ordersSell.Add(new Order()
            {
                CompanyName = "AMICA",
                PriceLimit = 1,
                Amount = 100,
                OrderType = Order.OrderTypes.Sell
            });
            ordersSell.Add(new Order()
            {
                CompanyName = "KGHM",
                PriceLimit = 1,
                Amount = 100,
                OrderType = Order.OrderTypes.Sell
            });

            IPortfolio portfolio = new BasicPortfolio(currentPriceProviderMock.Object, ChargeFunc);
            portfolio.Cash = 10000;

            orderMock.Setup(x => x.GenerateOrders(priceProvider, portfolio, dateFrom)).Returns(ordersBuy);
            orderMock.Setup(x => x.GenerateOrders(priceProvider, portfolio, dateTo)).Returns(ordersSell);

            IStrategy strategy = orderMock.Object;

            #endregion

            #region Act

            var result = runner.RunStrategy(strategy, portfolio, priceProvider, dateFrom, dateTo);

            #endregion

            #region Assert



            #endregion
        }
    }
}

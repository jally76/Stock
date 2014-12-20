using Moq;
using StockTools.Domain.Abstract;
using StockTools.Domain.Concrete;
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
        public void BasicTestRunnerTest_BasicPortfolio_BOSSAArchivePriceProvider_Simple_Positive()
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

            Mock<IArchivePriceProvider> archivePriceProviderMock = new Mock<IArchivePriceProvider>();
            archivePriceProviderMock.Setup(x => x.GetPriceByFullNameAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1.0);

            Mock<IStrategy> strategyMock = new Mock<IStrategy>();
            var ordersBuy = new List<Order>();
            //31.60 PLN
            ordersBuy.Add(new Order()
            {
                CompanyName = "AMICA",
                PriceLimit = 1000,
                Amount = 100,
                OrderType = Order.OrderTypes.Buy
            });
            //25.20 PLN
            ordersBuy.Add(new Order()
            {
                CompanyName = "KGHM",
                PriceLimit = 1000,
                Amount = 100,
                OrderType = Order.OrderTypes.Buy
            });

            var ordersSell = new List<Order>();
            //27.40 PLN
            ordersSell.Add(new Order()
            {
                CompanyName = "AMICA",
                PriceLimit = 1,
                Amount = 100,
                OrderType = Order.OrderTypes.Sell
            });
            //26.30
            ordersSell.Add(new Order()
            {
                CompanyName = "KGHM",
                PriceLimit = 1,
                Amount = 100,
                OrderType = Order.OrderTypes.Sell
            });

            IPortfolio portfolio = new BasicPortfolio(currentPriceProviderMock.Object, archivePriceProviderMock.Object, ChargeFunc);
            portfolio.Cash = 10000;

            strategyMock.Setup(x => x.GenerateOrders(priceProvider, portfolio, dateFrom)).Returns(ordersBuy);
            strategyMock.Setup(x => x.GenerateOrders(priceProvider, portfolio, dateTo)).Returns(ordersSell);

            IStrategy strategy = strategyMock.Object;

            #endregion

            #region Act

            var result = runner.RunStrategy(strategy, portfolio, priceProvider, dateFrom, dateTo);

            #endregion

            #region Assert

            var stockValueOnBuy = (31.6 * 100 + 25.2 * 100);
            var stockValueOnSell = (26.3 * 100 + 27.40 * 100);
            var expectedValue = (10000 - stockValueOnBuy) + stockValueOnSell;
            Assert.Equal(expectedValue, result);

            #endregion
        }

        [Fact]
        public void BasicTestRunnerTest_BasicPortfolio_BOSSAArchivePriceProvider_Simple_PartlyNegative()
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

            Mock<IArchivePriceProvider> archivePriceProviderMock = new Mock<IArchivePriceProvider>();
            archivePriceProviderMock.Setup(x => x.GetPriceByFullNameAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1.0);

            Mock<IStrategy> strategyMock = new Mock<IStrategy>();
            var ordersBuy = new List<Order>();
            //Value at day: 31.60 PLN
            ordersBuy.Add(new Order()
            {
                CompanyName = "AMICA",
                PriceLimit = 1000,
                Amount = 100,
                OrderType = Order.OrderTypes.Buy
            });
            //Value at day: 25.20 PLN
            //This order shouldn't be realised
            ordersBuy.Add(new Order()
            {
                CompanyName = "KGHM",
                PriceLimit = 20,
                Amount = 100,
                OrderType = Order.OrderTypes.Buy
            });

            var ordersSell = new List<Order>();
            //Value at day: 27.40 PLN
            ordersSell.Add(new Order()
            {
                CompanyName = "AMICA",
                PriceLimit = 1,
                Amount = 100,
                OrderType = Order.OrderTypes.Sell
            });
            //This order shouldn't be realised
            //Value at day: 26.30
            ordersSell.Add(new Order()
            {
                CompanyName = "KGHM",
                PriceLimit = 1,
                Amount = 100,
                OrderType = Order.OrderTypes.Sell
            });

            IPortfolio portfolio = new BasicPortfolio(currentPriceProviderMock.Object, archivePriceProviderMock.Object, ChargeFunc);
            portfolio.Cash = 10000;

            strategyMock.Setup(x => x.GenerateOrders(priceProvider, portfolio, dateFrom)).Returns(ordersBuy);
            strategyMock.Setup(x => x.GenerateOrders(priceProvider, portfolio, dateTo)).Returns(ordersSell);

            IStrategy strategy = strategyMock.Object;

            #endregion

            #region Act

            var result = runner.RunStrategy(strategy, portfolio, priceProvider, dateFrom, dateTo);

            #endregion

            #region Assert

            var stockValueOnBuy = (31.6* 100);
            var stockValueOnSell = (27.40 * 100);
            var expectedValue = (10000 - stockValueOnBuy) + stockValueOnSell;
            Assert.Equal(expectedValue, result);

            #endregion
        }
    }
}

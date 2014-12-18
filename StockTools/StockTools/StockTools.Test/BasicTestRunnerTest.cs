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
            
            var path = Environment.CurrentDirectory + "\\Files\\BOSSA\\Intraday\\";
            IArchivePriceProvider priceProvider = new BOSSAArchivePriceProvider(path);

            Mock<ICurrentPriceProvider> currentPriceProviderMock = new Mock<ICurrentPriceProvider>();
            currentPriceProviderMock.Setup(x => x.GetPriceByFullName(It.IsAny<string>())).Returns(1.0);
            currentPriceProviderMock.Setup(x => x.GetPriceByShortName(It.IsAny<string>())).Returns(1.0);

            Mock<IStrategy> orderMock = new Mock<IStrategy>();
            var ordersBuy = new List<Order>();
            ordersBuy.Add(new Order()
            {
                CompanyName = "MBANK",
                PriceLimit = 1000
            });
            ordersBuy.Add(new Order()
            {
                CompanyName = "MILENIUM",
                PriceLimit = 1000
            });

            IPortfolio portfolio = new BasicPortfolio(currentPriceProviderMock.Object, ChargeFunc);

            orderMock.Setup(x => x.GenerateOrders(priceProvider, portfolio, new DateTime(2001, 1, 3, 9, 0, 0))).Returns(ordersBuy);
            
            IStrategy strategy = orderMock.Object;
            

            #endregion

            #region Act


            #endregion

            #region Assert


            #endregion

        }
    }
}

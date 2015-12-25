using Moq;
using StockTools.Core.Interfaces;
using StockTools.Core.Models;
using StockTools.Core.Services;
using StockTools.Data.HistoricalData;
using StockTools.Data.SQL;
using StockTools.Infrastructure.Data;
using System;
using System.Collections.Generic;
using Xunit;

namespace StockTools.UnitTests
{
    public class StockSystemSimulatorTest
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
        public void StockSystemSimulator_DoesTickWork()
        {
            Mock<IDbHistoricalDataProvider> dbHistoricalDataProvider = new Mock<IDbHistoricalDataProvider>();
            IHistoricalPriceRepository historicalPriceRepository = new DbHistoricalPriceRepository(dbHistoricalDataProvider.Object);
            Mock<IOrderProcessor> orderProcessor = new Mock<IOrderProcessor>();
            IPortfolio portfolio = new Portfolio(ChargeFunc, 200);
            IStockSystemSimulator stockSystemSimulator = new StockSystemSimulator(new DateTime(2015, 10, 15), historicalPriceRepository, orderProcessor.Object, portfolio);

            Assert.Equal(2015, stockSystemSimulator.CurrentDate.Year);
            Assert.Equal(10, stockSystemSimulator.CurrentDate.Month);
            Assert.Equal(15, stockSystemSimulator.CurrentDate.Day);

            stockSystemSimulator.Tick(new TimeSpan(1, 0, 0, 0));

            Assert.Equal(2015, stockSystemSimulator.CurrentDate.Year);
            Assert.Equal(10, stockSystemSimulator.CurrentDate.Month);
            Assert.Equal(16, stockSystemSimulator.CurrentDate.Day);
        }

        [Fact]
        public void StockSystemSimulator_IsStockMarketAvailable()
        {
            Mock<IDbHistoricalDataProvider> dbHistoricalDataProvider = new Mock<IDbHistoricalDataProvider>();
            Mock<IOrderProcessor> orderProcessor = new Mock<IOrderProcessor>();
            dbHistoricalDataProvider.Setup(x => x.AnyTradingInDay(It.IsAny<DateTime>()))
                                    .Returns((DateTime d) => d == new DateTime(2015, 1, 15) ? true : false);
            IHistoricalPriceRepository historicalPriceRepository = new DbHistoricalPriceRepository(dbHistoricalDataProvider.Object);
            IPortfolio portfolio = new Portfolio(ChargeFunc, 200);

            IStockSystemSimulator stockSystemSimulator = new StockSystemSimulator(new DateTime(2015, 1, 15), historicalPriceRepository, orderProcessor.Object, portfolio);
            Assert.True(stockSystemSimulator.IsStockMarketAvailable);

            stockSystemSimulator = new StockSystemSimulator(new DateTime(2014, 12, 31), historicalPriceRepository, orderProcessor.Object, portfolio);
            Assert.False(stockSystemSimulator.IsStockMarketAvailable);
        }

        //[Fact]
        //public void StockSystemSimulator_SubmitOrder_Buy_AnyPrice_Positive()
        //{
        //    #region Arrange

        //    var order = new Order
        //        {
        //            OrderType = Order.OrderTypes.Buy,
        //            CompanyName = "Test",
        //            Amount = 1,
        //            AnyPrice = true
        //        };

        //    Mock<IDbHistoricalDataProvider> dbHistoricalDataProvider = new Mock<IDbHistoricalDataProvider>();
        //    dbHistoricalDataProvider.Setup(x => x.IsThereCompany("Test", new DateTime(2014, 10, 15)))
        //                            .Returns(true);
        //    var prices = new List<Price>();
        //    prices.Add(new Price
        //    {
        //        Company = new Company
        //        {
        //            Name = "Test"
        //        },
        //        DateTime = new DateTime(2014, 10, 15),
        //        Close = 1.0
        //    });
        //    dbHistoricalDataProvider.Setup(x => x.GetPriceListByDay("Test", new DateTime(2014, 10, 15)))
        //                            .Returns(prices);
        //    Mock<IOrderProcessor> orderProcessor = new Mock<IOrderProcessor>();
        //    orderProcessor.Setup(x => x.ProcessOrder(It.IsAny<Order>()))
        //                  .Returns(new Transaction { Amount = order.Amount,
        //                                             CompanyName = order.CompanyName,
        //                                             Price = 1.0,
        //                                             TransactionType = Transaction.TransactionTypes.Buy });
        //    IHistoricalPriceRepository historicalPriceRepository = new DbHistoricalPriceRepository(dbHistoricalDataProvider.Object);
        //    IPortfolio portfolio = new Portfolio(ChargeFunc, 200);
        //    IStockSystemSimulator stockSystemSimulator = new StockSystemSimulator(new DateTime(2014, 10, 15), historicalPriceRepository, orderProcessor.Object, portfolio);

        //    #endregion Arrange

        //    #region Act

        //    var result = stockSystemSimulator.SubmitOrder(order);

        //    #endregion Act

        //    #region Assert

        //    Assert.Equal(order.CompanyName, result.CompanyName);
        //    Assert.Equal(1.0, result.Price);
        //    Assert.Equal(Transaction.TransactionTypes.Buy, result.TransactionType);

        //    #endregion Assert
        //}
    }
}
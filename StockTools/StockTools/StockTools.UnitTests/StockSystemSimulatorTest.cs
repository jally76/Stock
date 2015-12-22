using Moq;
using StockTools.Core.Interfaces;
using StockTools.Core.Models;
using StockTools.Core.Services;
using StockTools.Data.HistoricalData;
using StockTools.Infrastructure.Data;
using System;
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
            Mock<IOrderProcessor> orderProcessor = new Mock<IOrderProcessor>();
            IHistoricalPriceRepository historicalPriceRepository = new DbHistoricalPriceRepository(dbHistoricalDataProvider.Object);
            IStockSystemSimulator stockSystemSimulator = new StockSystemSimulator(new DateTime(2015, 10, 15), historicalPriceRepository, orderProcessor.Object);

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

            IStockSystemSimulator stockSystemSimulator = new StockSystemSimulator(new DateTime(2015, 1, 15), historicalPriceRepository, orderProcessor.Object);
            Assert.True(stockSystemSimulator.IsStockMarketAvailable);

            stockSystemSimulator = new StockSystemSimulator(new DateTime(2014, 12, 31), historicalPriceRepository, orderProcessor.Object);
            Assert.False(stockSystemSimulator.IsStockMarketAvailable);
        }

        [Fact]
        public void StockSystemSimulator_SubmitOrder_Buy_AnyPrice_Positive()
        {
            var order = new Order
            {
                OrderType = Order.OrderTypes.Buy,
                CompanyName = "Test",
                Amount = 1,
                AnyPrice = true
            };

            Mock<IDbHistoricalDataProvider> dbHistoricalDataProvider = new Mock<IDbHistoricalDataProvider>();
            Mock<IOrderProcessor> orderProcessor = new Mock<IOrderProcessor>();
            IHistoricalPriceRepository historicalPriceRepository = new DbHistoricalPriceRepository(dbHistoricalDataProvider.Object);
            IStockSystemSimulator stockSystemSimulator = new StockSystemSimulator(new DateTime(2014, 10, 15), historicalPriceRepository, orderProcessor.Object);
            IPortfolio portfolio = new Portfolio(ChargeFunc, 200);
            var result = stockSystemSimulator.SubmitOrder(order, portfolio);
        }
    }
}
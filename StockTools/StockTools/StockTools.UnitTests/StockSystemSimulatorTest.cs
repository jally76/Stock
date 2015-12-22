using Moq;
using StockTools.Core.Interfaces;
using StockTools.Core.Services;
using StockTools.Data.HistoricalData;
using StockTools.Infrastructure.Data;
using System;
using Xunit;

namespace StockTools.UnitTests
{
    public class StockSystemSimulatorTest
    {
        [Fact]
        public void StockSystemSimulator_DoesTickWork()
        {
            Mock<IDbHistoricalDataProvider> dbHistoricalDataProvider = new Mock<IDbHistoricalDataProvider>();
            IHistoricalPriceRepository historicalPriceRepository = new DbHistoricalPriceRepository(dbHistoricalDataProvider.Object);
            IStockSystemSimulator stockSystemSimulator = new StockSystemSimulator(new DateTime(2015, 10, 15), historicalPriceRepository);

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
            dbHistoricalDataProvider.Setup(x => x.AnyTradingInDay(It.IsAny<DateTime>()))
                                    .Returns((DateTime d) => d == new DateTime(2015, 1, 15) ? true : false);

            IHistoricalPriceRepository historicalPriceRepository = new DbHistoricalPriceRepository(dbHistoricalDataProvider.Object);

            IStockSystemSimulator stockSystemSimulator = new StockSystemSimulator(new DateTime(2015, 1, 15), historicalPriceRepository);
            Assert.True(stockSystemSimulator.IsStockMarketAvailable);

            stockSystemSimulator = new StockSystemSimulator(new DateTime(2014, 12, 31), historicalPriceRepository);
            Assert.False(stockSystemSimulator.IsStockMarketAvailable);
        }
    }
}
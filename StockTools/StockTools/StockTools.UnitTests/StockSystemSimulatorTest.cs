using StockTools.Core.Interfaces;
using StockTools.Core.Services;
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
            IHistoricalPriceRepository historicalPriceRepository = new HistoricalPriceRepository();
            IStockSystemSimulator stockSystemSimulator = new StockSystemSimulator(new DateTime(2015, 10, 15), historicalPriceRepository);

            Assert.Equal(2015, stockSystemSimulator.CurrentDate.Year);
            Assert.Equal(10, stockSystemSimulator.CurrentDate.Month);
            Assert.Equal(15, stockSystemSimulator.CurrentDate.Day);

            stockSystemSimulator.Tick(new TimeSpan(1, 0, 0, 0));

            Assert.Equal(2015, stockSystemSimulator.CurrentDate.Year);
            Assert.Equal(10, stockSystemSimulator.CurrentDate.Month);
            Assert.Equal(16, stockSystemSimulator.CurrentDate.Day);
        }
    }
}
using StockTools.BusinessLogic.Concrete;
using StockTools.Data.HistoricalData;
using StockTools.Domain.Abstract;
using StockTools.Domain.Concrete;
using StockTools.Domain.Concrete.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StockTools.Test
{
    public class TestRunnerTest
    {
        public StockEntities DbContext { get; set; }
        public IHistoricalDataProvider HistoricalDataProvider { get; set; }
        public IPriceProvider PriceProvider { get; set; }
        public ITestRunner TestRunner { get; set; }

        public TestRunnerTest()
        {
            DbContext = new StockEntities();
            HistoricalDataProvider = new HistoricalDataProvider(DbContext);
            PriceProvider = new PriceProvider(HistoricalDataProvider);
            TestRunner = new TestRunner();
        }

        [Fact]
        public void TestRunner_BuyComarchAt2003AndSellItAt2008_Strategy()
        {
            var strategy = new BuyComarchAt2003AndSellItAt2008();
            var portfolio = new Portfolio(PriceProvider, ChargeFunc);
            portfolio.Cash = 10000;
            var result = TestRunner.RunStrategy(strategy, portfolio, PriceProvider, new DateTime(2002, 1, 1), new DateTime(2009, 1, 1), 1);
        }

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
    }
}

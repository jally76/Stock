using Moq;
using StockTools.Core.Interfaces;
using StockTools.Core.Models;
using StockTools.Core.Models.Delegates;
using StockTools.Core.Models.EventArgs;
using StockTools.Core.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace StockTools.UnitTests
{
    public class StrategyTestRunnerTest
    {
        private class TestStrategy : IStrategy
        {
            private IStockSystemSimulator _stockSystemSimulator;

            public IStockSystemSimulator StockSystemSimulator
            {
                get { return _stockSystemSimulator; }
                set { _stockSystemSimulator = value; }
            }

            public TestStrategy(IStockSystemSimulator stockSystemSimulator)
            {
                _stockSystemSimulator = stockSystemSimulator;
            }

            public List<Order> GenerateOrders()
            {
                var result = new List<Order>();

                if (_stockSystemSimulator.CurrentDate.Date == new DateTime(2014, 4, 3).Date)
                {
                    result.Add(new Order
                    {
                        AnyPrice = true,
                        CompanyName = "Test",
                        OrderType = Order.OrderTypes.Buy,
                        Amount = 1
                    });
                }
                if (_stockSystemSimulator.CurrentDate.Date == new DateTime(2014, 4, 7).Date)
                {
                    result.Add(new Order
                    {
                        AnyPrice = true,
                        CompanyName = "Test",
                        OrderType = Order.OrderTypes.Sell,
                        Amount = 1
                    });
                }

                return result;
            }
        }

        private class TestOrderProcessor : IOrderProcessor
        {
            public Transaction ProcessOrder(Order order)
            {
                return new Transaction
                {
                    Amount = order.Amount,
                    CompanyName = order.CompanyName,
                    Price = 150,
                    TransactionType = order.OrderType == Order.OrderTypes.Buy ? Transaction.TransactionTypes.Buy : Transaction.TransactionTypes.Sell
                };
            }

            public event OrderProcessedDelegate OrderProcessed;

            private List<Order> _ordersToBeProcessed;

            public void OnCurrentDateChanged(object sender, EventArgs e)
            {
                var ordersToBeRemoved = new List<Order>();
                foreach (var order in _ordersToBeProcessed)
                {
                    //TODO Here should be some logic for processing order
                    OrderProcessed(this, new OrderEventArgs(new Transaction
                    {
                        Amount = order.Amount,
                        CompanyName = order.CompanyName,
                        Price = 150,
                        TransactionType = order.OrderType == Order.OrderTypes.Buy ? Transaction.TransactionTypes.Buy : Transaction.TransactionTypes.Sell
                    }));
                    ordersToBeRemoved.Add(order);
                }
                foreach (var order in ordersToBeRemoved)
                {
                    _ordersToBeProcessed.Remove(order);
                }
            }

            public void Enqueue(Order order)
            {
                if (_ordersToBeProcessed == null)
                {
                    _ordersToBeProcessed = new List<Order>();
                }
                _ordersToBeProcessed.Add(order);
            }
        }

        [Fact]
        public void StrategyTestRunner_Run()
        {
            #region Arrange

            Mock<IHistoricalPriceRepository> historicalPriceRepositoryMock = new Mock<IHistoricalPriceRepository>();
            historicalPriceRepositoryMock.Setup(x => x.IsThereCompany("Test", It.IsAny<DateTime>()))
                                         .Returns(true);
            historicalPriceRepositoryMock.Setup(x => x.GetClosest("Test", new DateTime(2014, 4, 3)))
                                        .Returns(150);
            historicalPriceRepositoryMock.Setup(x => x.GetClosest("Test", new DateTime(2014, 4, 4)))
                                        .Returns(151);
            historicalPriceRepositoryMock.Setup(x => x.GetClosest("Test", new DateTime(2014, 4, 5)))
                                        .Returns(152);
            historicalPriceRepositoryMock.Setup(x => x.GetClosest("Test", new DateTime(2014, 4, 6)))
                                        .Returns(153);
            historicalPriceRepositoryMock.Setup(x => x.GetClosest("Test", new DateTime(2014, 4, 7)))
                                        .Returns(154);

            IPortfolio portfolio = new Portfolio(Utils.Instance.ChargeFunc, 200.0);
            IOrderProcessor orderProcessor = new TestOrderProcessor();
            IStockSystemSimulator stockSystemSimulator = new StockSystemSimulator(new DateTime(2014, 4, 3), historicalPriceRepositoryMock.Object, orderProcessor, portfolio);

            Mock<IProfitCalculator> profitCalculatorMock = new Mock<IProfitCalculator>();
            profitCalculatorMock.Setup(x => x.GetGrossProfit(It.IsAny<List<Transaction>>(), It.IsAny<List<Dividend>>(), It.IsAny<List<InvestmentPortfolioItem>>(), new DateTime(2014, 4, 3)))
                                .Returns(150);
            profitCalculatorMock.Setup(x => x.GetGrossProfit(It.IsAny<List<Transaction>>(), It.IsAny<List<Dividend>>(), It.IsAny<List<InvestmentPortfolioItem>>(), new DateTime(2014, 4, 4)))
                                .Returns(151);
            profitCalculatorMock.Setup(x => x.GetGrossProfit(It.IsAny<List<Transaction>>(), It.IsAny<List<Dividend>>(), It.IsAny<List<InvestmentPortfolioItem>>(), new DateTime(2014, 4, 5)))
                                .Returns(152);
            profitCalculatorMock.Setup(x => x.GetGrossProfit(It.IsAny<List<Transaction>>(), It.IsAny<List<Dividend>>(), It.IsAny<List<InvestmentPortfolioItem>>(), new DateTime(2014, 4, 6)))
                                .Returns(153);
            profitCalculatorMock.Setup(x => x.GetGrossProfit(It.IsAny<List<Transaction>>(), It.IsAny<List<Dividend>>(), It.IsAny<List<InvestmentPortfolioItem>>(), new DateTime(2014, 4, 7)))
                                .Returns(154);

            IStrategyTestRunner strategyTestRunner = new StrategyTestRunner(historicalPriceRepositoryMock.Object, portfolio, stockSystemSimulator, profitCalculatorMock.Object);
            IStrategy strategy = new TestStrategy(stockSystemSimulator);

            #endregion Arrange

            #region Act

            var result = strategyTestRunner.Run(strategy, new DateTime(2014, 4, 3), new DateTime(2014, 4, 7), new TimeSpan(1, 0, 0, 0));

            #endregion Act

            #region Assert

            Assert.Equal(150.0, result[new DateTime(2014, 4, 3)]);
            Assert.Equal(151.0, result[new DateTime(2014, 4, 4)]);
            Assert.Equal(152.0, result[new DateTime(2014, 4, 5)]);
            Assert.Equal(153.0, result[new DateTime(2014, 4, 6)]);
            Assert.Equal(154.0, result[new DateTime(2014, 4, 7)]);

            #endregion Assert
        }
    }
}
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

namespace StockTools.Test.Portfolio
{
    public class BasicPortfolioTest
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
        void BasicPortfolioTest_AddTransaction_Simple_Positive()
        {
            #region Arrange

            Mock<ICurrentPriceProvider> currentPriceProviderMock = new Mock<ICurrentPriceProvider>();
            currentPriceProviderMock.Setup(x => x.GetPriceByFullName(It.IsAny<string>())).Returns(1.0);
            currentPriceProviderMock.Setup(x => x.GetPriceByShortName(It.IsAny<string>())).Returns(1.0);

            Mock<IArchivePriceProvider> archivePriceProviderMock = new Mock<IArchivePriceProvider>();
            archivePriceProviderMock.Setup(x => x.GetPriceByFullNameAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1.0);

            var portfolio = new BasicPortfolio(currentPriceProviderMock.Object, archivePriceProviderMock.Object, ChargeFunc);
            portfolio.Cash = 10000;


            var firstTransaction = new Transaction()
            {
                Amount = 5,
                CompanyName = "AMICA",
                Price = 10,
                Time = new DateTime(2000, 1, 1, 9, 0, 0),
                TransactionType = Transaction.TransactionTypes.Buy
            };

            var secondTransaction = new Transaction()
            {
                Amount = 5,
                CompanyName = "AMICA",
                Price = 10,
                Time = new DateTime(2001, 1, 1, 9, 0, 0),
                TransactionType = Transaction.TransactionTypes.Sell
            };

            #endregion

            #region Act

            portfolio.AddTransaction(firstTransaction);
            var cashAfterFirstTransaction = portfolio.Cash;
            var itemsCountAfterFirstTransaction = portfolio.Items.Where(x => x.CompanyName == firstTransaction.CompanyName).Single().NumberOfShares;
            portfolio.AddTransaction(secondTransaction);
            var cashAfterSecondTransaction = portfolio.Cash;
            var itemsCountAfteSecondTransaction = portfolio.Items.Where(x => x.CompanyName == firstTransaction.CompanyName).Single().NumberOfShares;

            #endregion

            #region Assert

            Assert.Equal(10000 - 10 * 5, cashAfterFirstTransaction);
            Assert.Equal(10000, cashAfterSecondTransaction);
            Assert.Equal(5, itemsCountAfterFirstTransaction);
            Assert.Equal(0, itemsCountAfteSecondTransaction);

            #endregion
        }

        [Fact]
        void BasicPortfolioTest_AddTransaction_Simple_PartlyPositive()
        {
            #region Arrange

            Mock<ICurrentPriceProvider> currentPriceProviderMock = new Mock<ICurrentPriceProvider>();
            currentPriceProviderMock.Setup(x => x.GetPriceByFullName(It.IsAny<string>())).Returns(1.0);
            currentPriceProviderMock.Setup(x => x.GetPriceByShortName(It.IsAny<string>())).Returns(1.0);

            Mock<IArchivePriceProvider> archivePriceProviderMock = new Mock<IArchivePriceProvider>();
            archivePriceProviderMock.Setup(x => x.GetPriceByFullNameAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1.0);

            var portfolio = new BasicPortfolio(currentPriceProviderMock.Object, archivePriceProviderMock.Object, ChargeFunc);
            portfolio.Cash = 10000;


            var firstTransaction = new Transaction()
            {
                Amount = 5,
                CompanyName = "AMICA",
                Price = 10,
                Time = new DateTime(2000, 1, 1, 9, 0, 0),
                TransactionType = Transaction.TransactionTypes.Buy
            };

            var secondTransaction = new Transaction()
            {
                Amount = 3,
                CompanyName = "AMICA",
                Price = 10,
                Time = new DateTime(2001, 1, 1, 9, 0, 0),
                TransactionType = Transaction.TransactionTypes.Sell
            };

            #endregion

            #region Act

            portfolio.AddTransaction(firstTransaction);
            var cashAfterFirstTransaction = portfolio.Cash;
            var itemsCountAfterFirstTransaction = portfolio.Items.Where(x => x.CompanyName == firstTransaction.CompanyName).Single().NumberOfShares;
            portfolio.AddTransaction(secondTransaction);
            var cashAfterSecondTransaction = portfolio.Cash;
            var itemsCountAfteSecondTransaction = portfolio.Items.Where(x => x.CompanyName == firstTransaction.CompanyName).Single().NumberOfShares;

            #endregion

            #region Assert

            Assert.Equal(10000 - 10 * 5, cashAfterFirstTransaction);
            Assert.Equal(10000 - 10 * 2, cashAfterSecondTransaction);
            Assert.Equal(5, itemsCountAfterFirstTransaction);
            Assert.Equal(2, itemsCountAfteSecondTransaction);

            #endregion
        }

        [Fact]
        void BasicPortfolioTest_AddTransaction_Simple_Negative()
        {
            #region Arrange

            Mock<ICurrentPriceProvider> currentPriceProviderMock = new Mock<ICurrentPriceProvider>();
            currentPriceProviderMock.Setup(x => x.GetPriceByFullName(It.IsAny<string>())).Returns(1.0);
            currentPriceProviderMock.Setup(x => x.GetPriceByShortName(It.IsAny<string>())).Returns(1.0);

            Mock<IArchivePriceProvider> archivePriceProviderMock = new Mock<IArchivePriceProvider>();
            archivePriceProviderMock.Setup(x => x.GetPriceByFullNameAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1.0);

            var portfolio = new BasicPortfolio(currentPriceProviderMock.Object, archivePriceProviderMock.Object, ChargeFunc);
            portfolio.Cash = 10000;


            var firstTransaction = new Transaction()
            {
                Amount = 5,
                CompanyName = "AMICA",
                Price = 10,
                Time = new DateTime(2000, 1, 1, 9, 0, 0),
                TransactionType = Transaction.TransactionTypes.Buy
            };

            var secondTransaction = new Transaction()
            {
                Amount = 10,
                CompanyName = "AMICA",
                Price = 10,
                Time = new DateTime(2001, 1, 1, 9, 0, 0),
                TransactionType = Transaction.TransactionTypes.Sell
            };

            #endregion

            #region Act

            portfolio.AddTransaction(firstTransaction);
            var cashAfterFirstTransaction = portfolio.Cash;
            var itemsCountAfterFirstTransaction = portfolio.Items.Where(x => x.CompanyName == firstTransaction.CompanyName).Single().NumberOfShares;
            portfolio.AddTransaction(secondTransaction);
            var cashAfterSecondTransaction = portfolio.Cash;
            var itemsCountAfteSecondTransaction = portfolio.Items.Where(x => x.CompanyName == firstTransaction.CompanyName).Single().NumberOfShares;

            #endregion

            #region Assert

            Assert.Equal(10000 - 10 * 5, cashAfterFirstTransaction);
            Assert.Equal(10000 - 10 * 5, cashAfterSecondTransaction);
            Assert.Equal(5, itemsCountAfterFirstTransaction);
            Assert.Equal(5, itemsCountAfteSecondTransaction);

            #endregion
        }

        [Fact]
        void BasicPortfolioTest_AddTransaction_MultipleBuy()
        {
            #region Arrange

            Mock<ICurrentPriceProvider> currentPriceProviderMock = new Mock<ICurrentPriceProvider>();
            currentPriceProviderMock.Setup(x => x.GetPriceByFullName(It.IsAny<string>())).Returns(1.0);
            currentPriceProviderMock.Setup(x => x.GetPriceByShortName(It.IsAny<string>())).Returns(1.0);

            Mock<IArchivePriceProvider> archivePriceProviderMock = new Mock<IArchivePriceProvider>();
            archivePriceProviderMock.Setup(x => x.GetPriceByFullNameAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1.0);

            var portfolio = new BasicPortfolio(currentPriceProviderMock.Object, archivePriceProviderMock.Object, ChargeFunc);
            portfolio.Cash = 10000;


            var firstTransaction = new Transaction()
            {
                Amount = 5,
                CompanyName = "AMICA",
                Price = 10,
                Time = new DateTime(2000, 1, 1, 9, 0, 0),
                TransactionType = Transaction.TransactionTypes.Buy
            };

            var secondTransaction = new Transaction()
            {
                Amount = 10,
                CompanyName = "AMICA",
                Price = 99,
                Time = new DateTime(2001, 1, 1, 9, 0, 0),
                TransactionType = Transaction.TransactionTypes.Buy
            };

            #endregion

            #region Act

            portfolio.AddTransaction(firstTransaction);
            var cashAfterFirstTransaction = portfolio.Cash;
            var itemsCountAfterFirstTransaction = portfolio.Items.Where(x => x.CompanyName == firstTransaction.CompanyName).Single().NumberOfShares;
            portfolio.AddTransaction(secondTransaction);
            var cashAfterSecondTransaction = portfolio.Cash;
            var itemsCountAfteSecondTransaction = portfolio.Items.Where(x => x.CompanyName == firstTransaction.CompanyName).Single().NumberOfShares;

            #endregion

            #region Assert

            Assert.Equal(10000 - 10 * 5, cashAfterFirstTransaction);
            Assert.Equal(cashAfterFirstTransaction - 99 * 10, cashAfterSecondTransaction);
            Assert.Equal(5, itemsCountAfterFirstTransaction);
            Assert.Equal(15, itemsCountAfteSecondTransaction);

            #endregion
        }

    }
}


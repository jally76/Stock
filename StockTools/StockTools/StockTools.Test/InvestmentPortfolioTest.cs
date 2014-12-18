using Moq;
using StockTools.BusinessLogic.Abstract;
using StockTools.BusinessLogic.Concrete;
using StockTools.Domain.Abstract;
using StockTools.Domain.Concrete;
using StockTools.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using System.Linq;

namespace StockTools.Test
{
    public class InvestmentPortfolioTest
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
        public void InvestmentPortfolio_GetRealisedGrossProfit_Simple()
        {
            #region Arrange

            Mock<ICurrentPriceProvider> mock = new Mock<ICurrentPriceProvider>();
            mock.Setup(x => x.GetPriceByFullName(It.IsAny<string>())).Returns(1.0);
            //mock.Setup(x => x.GetPriceByFullNameAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1.0);
            mock.Setup(x => x.GetPriceByShortName(It.IsAny<string>())).Returns(1.0);
            //mock.Setup(x => x.GetPriceByShortNameAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1.0);

            IPortfolio _investmentPortfolio = new BasicPortfolio(mock.Object, ChargeFunc);

            var transactions = new List<Transaction>(4);
            transactions.Add(new Transaction()
            {
                Time = new DateTime(2014, 7, 9, 9, 29, 10),
                CompanyName = "MBANK",
                TransactionType = Transaction.TransactionTypes.Sell,
                Amount = 1,
                Price = 485.05,
            });
            transactions.Add(new Transaction()
            {
                Time = new DateTime(2014, 7, 7, 14, 5, 0),
                CompanyName = "MILLENNIUM",
                TransactionType = Transaction.TransactionTypes.Buy,
                Amount = 100,
                Price = 8.05,
            });
            transactions.Add(new Transaction()
            {
                Time = new DateTime(2014, 7, 7, 9, 20, 0),
                CompanyName = "CORMAY",
                TransactionType = Transaction.TransactionTypes.Buy,
                Amount = 100,
                Price = 5.47,
            });
            transactions.Add(new Transaction()
            {
                Time = new DateTime(2014, 7, 4, 9, 18, 00),
                CompanyName = "MBANK",
                TransactionType = Transaction.TransactionTypes.Buy,
                Amount = 1,
                Price = 500.0,
            });

            _investmentPortfolio.Transactions = transactions;

            #endregion

            #region Act

            double result = _investmentPortfolio.GetRealisedGrossProfit();

            #endregion

            #region Assert

            Assert.Equal(Math.Round(result, 2), Math.Round(-20.95, 2));

            #endregion
        }

        [Fact]
        public void InvestmentPortfolio_GetRealisedGrossProfit_Complex()
        {
            #region Arrange

            Mock<ICurrentPriceProvider> mock = new Mock<ICurrentPriceProvider>();
            mock.Setup(x => x.GetPriceByFullName(It.IsAny<string>())).Returns(1.0);
            //mock.Setup(x => x.GetPriceByFullNameAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1.0);
            mock.Setup(x => x.GetPriceByShortName(It.IsAny<string>())).Returns(1.0);
            //mock.Setup(x => x.GetPriceByShortNameAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1.0);

            IPortfolio _investmentPortfolio = new BasicPortfolio(mock.Object, ChargeFunc);

            var transactions = new List<Transaction>(9);

            #region Description

            //Name Type Amount Price Charge
            //First
            //A BUY 2 499 3,8922
            //A BUY 1 502 3
            //B BUY 14 81 4,4226
            //C BUY 100 1,28 3
            //A SELL 1 520 3
            //B SELL 14 60 3,276
            //C BUY 20 1,31 3
            //C SELL 119 1,35 3
            //C SELL 1 1,35 3
            //C BUY 128 1,0 3
            //Last

            //Name Type Amount Price
            //A BUY = 3 1500
            //A SELL = 1 502
            //charges = 3,8922 + 3 + 3
            //A DIFF = 520 - 500 = 20 - charges
            //sum = 10,1078

            //B BUY = 14 1134
            //B SELL = 14 840
            //charges = 4,4226 + 3,276 = 7,6986
            //B DIFF = 840 - 1134 = -294 - charges
            //sum = -301,6986

            //C BUY = 120 154,2
            //C SELL = 120 162
            //charges = 3 + 3 + 3 + 3
            //C DIFF = 162 - 154,2 = 7,8 - charges
            //sum = -4,2

            //SUM = -295,7908
            //sum charges = 29,5908

            #endregion

            transactions.Add(new Transaction()
            {
                Time = new DateTime(2014, 3, 4, 9, 29, 0),
                CompanyName = "C",
                TransactionType = Transaction.TransactionTypes.Buy,
                Amount = 128,
                Price = 1.0,
            });

            transactions.Add(new Transaction()
            {
                Time = new DateTime(2014, 3, 3, 11, 29, 0),
                CompanyName = "C",
                TransactionType = Transaction.TransactionTypes.Sell,
                Amount = 1,
                Price = 1.35,
            });

            transactions.Add(new Transaction()
            {
                Time = new DateTime(2014, 3, 3, 10, 29, 0),
                CompanyName = "C",
                TransactionType = Transaction.TransactionTypes.Sell,
                Amount = 119,
                Price = 1.35,
            });

            transactions.Add(new Transaction()
            {
                Time = new DateTime(2014, 3, 3, 9, 29, 0),
                CompanyName = "C",
                TransactionType = Transaction.TransactionTypes.Buy,
                Amount = 20,
                Price = 1.31,
            });

            transactions.Add(new Transaction()
            {
                Time = new DateTime(2014, 2, 15, 9, 2, 0),
                CompanyName = "B",
                TransactionType = Transaction.TransactionTypes.Sell,
                Amount = 14,
                Price = 60.0,
            });

            transactions.Add(new Transaction()
            {
                Time = new DateTime(2014, 2, 14, 9, 2, 0),
                CompanyName = "A",
                TransactionType = Transaction.TransactionTypes.Sell,
                Amount = 1,
                Price = 520.0,
            });

            transactions.Add(new Transaction()
            {
                Time = new DateTime(2014, 1, 3, 9, 29, 0),
                CompanyName = "C",
                TransactionType = Transaction.TransactionTypes.Buy,
                Amount = 100,
                Price = 1.28,
            });

            transactions.Add(new Transaction()
            {
                Time = new DateTime(2014, 1, 2, 9, 2, 0),
                CompanyName = "B",
                TransactionType = Transaction.TransactionTypes.Buy,
                Amount = 14,
                Price = 81.0,
            });

            transactions.Add(new Transaction()
            {
                Time = new DateTime(2014, 1, 1, 9, 2, 0),
                CompanyName = "A",
                TransactionType = Transaction.TransactionTypes.Buy,
                Amount = 1,
                Price = 502.0,
            });

            transactions.Add(new Transaction()
            {
                Time = new DateTime(2014, 1, 1, 9, 1, 0),
                CompanyName = "A",
                TransactionType = Transaction.TransactionTypes.Buy,
                Amount = 2,
                Price = 499.0,
            });

            _investmentPortfolio.Transactions = transactions;

            #endregion

            #region Act

            double result = _investmentPortfolio.GetRealisedGrossProfit();

            #endregion

            #region Assert

            Assert.Equal(Math.Round(-295.7908, 4), Math.Round(result, 4));

            #endregion
        }

        [Fact]
        public void InvestmentPortfolio_GetRealisedGrossProfit_UsingDate_And_UsingFile()
        {
            #region Arrange

            Mock<ICurrentPriceProvider> mock = new Mock<ICurrentPriceProvider>();
            mock.Setup(x => x.GetPriceByFullName(It.IsAny<string>())).Returns(1.0);
            //mock.Setup(x => x.GetPriceByFullNameAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1.0);
            mock.Setup(x => x.GetPriceByShortName(It.IsAny<string>())).Returns(1.0);
            //mock.Setup(x => x.GetPriceByShortNameAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1.0);

            IPortfolio _investmentPortfolio = new BasicPortfolio(mock.Object, ChargeFunc);

            IBookkeepingService _bookkeepingService = new MbankBookkeepingService();
            var path = Environment.CurrentDirectory + "\\Files\\transactions3.csv";
            var file = File.ReadAllBytes(path);
            MemoryStream stream = new MemoryStream(file);

            var transactions = _bookkeepingService.ReadTransactionHistory(stream);
            _investmentPortfolio.Transactions = transactions;

            #endregion

            #region Act

            var date = new DateTime(2014,7,9).AddHours(9).AddMinutes(29).AddSeconds(10);
            double result = _investmentPortfolio.GetRealisedGrossProfit(date);

            #endregion

            #region Assert

            Assert.Equal(Math.Round(-20.95, 4), Math.Round(result, 4));

            #endregion
        }

        [Fact]
        public void InvestmentPortfolio_GetRealisedNetProfit_Using_File()
        {
            #region Arrange

            Mock<ICurrentPriceProvider> mock = new Mock<ICurrentPriceProvider>();
            mock.Setup(x => x.GetPriceByFullName(It.IsAny<string>())).Returns(1.0);
            //mock.Setup(x => x.GetPriceByFullNameAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1.0);
            mock.Setup(x => x.GetPriceByShortName(It.IsAny<string>())).Returns(1.0);
            //mock.Setup(x => x.GetPriceByShortNameAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1.0);

            IPortfolio _investmentPortfolio = new BasicPortfolio(mock.Object, ChargeFunc);

            IBookkeepingService _bookkeepingService = new MbankBookkeepingService();
            var path = Environment.CurrentDirectory + "\\Files\\transactions2.csv";
            var file = File.ReadAllBytes(path);
            MemoryStream stream = new MemoryStream(file);

            var transactions = _bookkeepingService.ReadTransactionHistory(stream);

            #endregion

            #region Act

            _investmentPortfolio.Transactions = transactions;
            double result = _investmentPortfolio.GetRealisedGrossProfit();

            #endregion

            #region Assert

            Assert.Equal(Math.Round(result, 2), Math.Round(88.0, 2));

            #endregion
        }

        [Fact]
        public void InvestmentPortfolio_GetPairedTransactions()
        {
            #region Arrange

            Mock<ICurrentPriceProvider> mock = new Mock<ICurrentPriceProvider>();
            mock.Setup(x => x.GetPriceByFullName(It.IsAny<string>())).Returns(1.0);
            //mock.Setup(x => x.GetPriceByFullNameAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1.0);
            mock.Setup(x => x.GetPriceByShortName(It.IsAny<string>())).Returns(1.0);
            //mock.Setup(x => x.GetPriceByShortNameAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1.0);

            IPortfolio _investmentPortfolio = new BasicPortfolio(mock.Object, ChargeFunc);

            var transactions = new List<Transaction>(4);
            transactions.Add(new Transaction()
            {
                Time = new DateTime(2014, 7, 9, 9, 29, 10),
                CompanyName = "MBANK",
                //TransactionName = "SPRZEDAŻ",
                TransactionType = Transaction.TransactionTypes.Sell,
                Amount = 1,
                Price = 485.05,
                //TotalValue = 485.05
            });
            transactions.Add(new Transaction()
            {
                Time = new DateTime(2014, 7, 7, 14, 5, 0),
                CompanyName = "MILLENNIUM",
                //TransactionName = "KUPNO",
                TransactionType = Transaction.TransactionTypes.Buy,
                Amount = 100,
                Price = 8.05,
                //TotalValue = 805.0
            });
            transactions.Add(new Transaction()
            {
                Time = new DateTime(2014, 7, 7, 9, 20, 0),
                CompanyName = "CORMAY",
                //TransactionName = "KUPNO",
                TransactionType = Transaction.TransactionTypes.Buy,
                Amount = 100,
                Price = 5.47,
                //TotalValue = 547.0
            });
            transactions.Add(new Transaction()
            {
                Time = new DateTime(2014, 7, 4, 9, 18, 00),
                CompanyName = "MBANK",
                //TransactionName = "KUPNO",
                TransactionType = Transaction.TransactionTypes.Buy,
                Amount = 1,
                Price = 500.0,
                //TotalValue = 500.0
            });

            _investmentPortfolio.Transactions = transactions;

            

            #endregion

            #region Act

            var result = _investmentPortfolio.GetPairedTransactions();

            #endregion

            #region Assert

            result.Any(x => Helper.ArePropertiesEqual(x, transactions[0]));
            result.Any(x => Helper.ArePropertiesEqual(x, transactions[3]));

            #endregion
        }

        [Fact]
        public void InvestmentPortfolio_GetTransactionPair()
        {
            #region Arrange

            Mock<ICurrentPriceProvider> mock = new Mock<ICurrentPriceProvider>();
            mock.Setup(x => x.GetPriceByFullName(It.IsAny<string>())).Returns(1.0);
            //mock.Setup(x => x.GetPriceByFullNameAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1.0);
            mock.Setup(x => x.GetPriceByShortName(It.IsAny<string>())).Returns(1.0);
            //mock.Setup(x => x.GetPriceByShortNameAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1.0);

            IPortfolio _investmentPortfolio = new BasicPortfolio(mock.Object, ChargeFunc);

            var transactions = new List<Transaction>(4);
            transactions.Add(new Transaction()
            {
                Time = new DateTime(2014, 7, 9, 9, 29, 10),
                CompanyName = "MBANK",
                //TransactionName = "SPRZEDAŻ",
                TransactionType = Transaction.TransactionTypes.Sell,
                Amount = 1,
                Price = 485.05,
                //TotalValue = 485.05
            });
            transactions.Add(new Transaction()
            {
                Time = new DateTime(2014, 7, 7, 14, 5, 0),
                CompanyName = "MILLENNIUM",
                //TransactionName = "KUPNO",
                TransactionType = Transaction.TransactionTypes.Buy,
                Amount = 100,
                Price = 8.05,
                //TotalValue = 805.0
            });
            transactions.Add(new Transaction()
            {
                Time = new DateTime(2014, 7, 7, 9, 20, 0),
                CompanyName = "CORMAY",
                //TransactionName = "KUPNO",
                TransactionType = Transaction.TransactionTypes.Buy,
                Amount = 100,
                Price = 5.47,
                //TotalValue = 547.0
            });
            transactions.Add(new Transaction()
            {
                Time = new DateTime(2014, 7, 4, 9, 18, 00),
                CompanyName = "MBANK",
                //TransactionName = "KUPNO",
                TransactionType = Transaction.TransactionTypes.Buy,
                Amount = 1,
                Price = 500.0,
                //TotalValue = 500.0
            });

            _investmentPortfolio.Transactions = transactions;

            #endregion

            #region Act

            var result = _investmentPortfolio.GetTransactionPair(transactions[3]);

            #endregion

            #region Assert

            Assert.True(Helper.ArePropertiesEqual(result, transactions[0]));

            #endregion
        }
    }
}

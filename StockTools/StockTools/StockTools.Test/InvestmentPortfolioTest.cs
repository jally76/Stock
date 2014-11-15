﻿using Moq;
using StockTools.BusinessLogic.Abstract;
using StockTools.BusinessLogic.Concrete;
using StockTools.Entities;
using System;
using System.Collections.Generic;
using Xunit;

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
        public void InvestmentPortfolio_GetRealisedNetProfit()
        {
            #region Arrange

            Mock<IPriceService> mock = new Mock<IPriceService>();
            mock.Setup(x => x.GetCompanyPriceByFullName(It.IsAny<string>())).Returns(1.0);
            mock.Setup(x => x.GetCompanyPriceByFullNameAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1.0);
            mock.Setup(x => x.GetCompanyPriceByShortName(It.IsAny<string>())).Returns(1.0);
            mock.Setup(x => x.GetCompanyPriceByShortNameAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1.0);

            IInvestmentPortfolio _investmentPortfolio = new InvestmentPortfolio(mock.Object);

            var transactions = new List<Transaction>(4);
            transactions.Add(new Transaction()
            {
                DateAndTime = new DateTime(2014, 7, 9, 9, 29, 10),
                CompanyName = "MBANK",
                TransactionName = "SPRZEDAŻ",
                TransactionType = Transaction.TransactionTypes.Sell,
                Amount = 1,
                Price = 485.05,
                TotalValue = 485.05
            });
            transactions.Add(new Transaction()
            {
                DateAndTime = new DateTime(2014, 7, 7, 14, 5, 0),
                CompanyName = "MILLENNIUM",
                TransactionName = "KUPNO",
                TransactionType = Transaction.TransactionTypes.Buy,
                Amount = 100,
                Price = 8.05,
                TotalValue = 805.0
            });
            transactions.Add(new Transaction()
            {
                DateAndTime = new DateTime(2014, 7, 7, 9, 20, 0),
                CompanyName = "CORMAY",
                TransactionName = "KUPNO",
                TransactionType = Transaction.TransactionTypes.Buy,
                Amount = 100,
                Price = 5.47,
                TotalValue = 547.0
            });
            transactions.Add(new Transaction()
            {
                DateAndTime = new DateTime(2014, 7, 4, 9, 18, 00),
                CompanyName = "MBANK",
                TransactionName = "KUPNO",
                TransactionType = Transaction.TransactionTypes.Buy,
                Amount = 1,
                Price = 500.0,
                TotalValue = 500.0
            });

            _investmentPortfolio.Transactions = transactions;

            Func<double, double> chargeFunc = ChargeFunc;
            _investmentPortfolio.ChargeFunction = chargeFunc;

            #endregion

            #region Act

            double result = _investmentPortfolio.GetRealisedGrossProfit();

            #endregion

            #region Assert

            Assert.Equal(Math.Round(result, 2), Math.Round(-20.95, 2));

            #endregion
        }
    }
}

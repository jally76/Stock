using StockTools.Core.Interfaces;
using StockTools.Core.Models;
using StockTools.Core.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StockTools.UnitTests
{
    public class Portfolio
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

        /// <summary>
        /// This simple transaction should be added without any problems
        /// </summary>
        [Fact]
        private void Portfolio_AddTransaction_Simple_Succ()
        {
            #region Arrange

            IPortfolio _investmentPortfolio = new StockTools.Core.Services.Portfolio(ChargeFunc, 200.0);

            var transaction = new Transaction()
            {
                Time = new DateTime(2014, 3, 4, 9, 29, 0),
                CompanyId = new Guid(),
                CompanyName = "C",
                TransactionType = Transaction.TransactionTypes.Buy,
                Amount = 128,
                Price = 1.0,
            };

            #endregion

            #region Act

            _investmentPortfolio.AddTransaction(transaction);

            #endregion

            #region Assert

            Assert.True(_investmentPortfolio.Transactions.Count > 0);
            Assert.True(_investmentPortfolio.Transactions.Count == 1);

            Assert.Equal(transaction.Time, _investmentPortfolio.Transactions.First().Time);
            Assert.Equal(transaction.CompanyId, _investmentPortfolio.Transactions.First().CompanyId);
            Assert.Equal(transaction.CompanyName, _investmentPortfolio.Transactions.First().CompanyName);
            Assert.Equal(transaction.TransactionType, _investmentPortfolio.Transactions.First().TransactionType);
            Assert.Equal(transaction.Amount, _investmentPortfolio.Transactions.First().Amount);
            Assert.Equal(transaction.Price, _investmentPortfolio.Transactions.First().Price);

            #endregion
        }

        /// <summary>
        /// Add transaction method should throw exception when there's not enough money
        /// </summary>
        [Fact]
        private void Portfolio_AddTransaction_Simple_Fail()
        {
            #region Arrange

            IPortfolio _investmentPortfolio = new StockTools.Core.Services.Portfolio(ChargeFunc, 0.0);

            var transaction = new Transaction()
            {
                Time = new DateTime(2014, 3, 4, 9, 29, 0),
                CompanyId = new Guid(),
                CompanyName = "C",
                TransactionType = Transaction.TransactionTypes.Buy,
                Amount = 128,
                Price = 1.0,
            };

            #endregion

            #region Act+Assert

            Assert.Throws<NotEnoughMoneyException>(() => _investmentPortfolio.AddTransaction(transaction));

            #endregion
        }

        [Fact]
        private void Portfolio_AddTransaction()
        {
            IPortfolio _investmentPortfolio = new StockTools.Core.Services.Portfolio(ChargeFunc, 0.0);

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

            #endregion Description

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

            //_investmentPortfolio.Transactions = transactions;
            foreach (var transaction in transactions.OrderBy(x => x.Time))
            {
                _investmentPortfolio.AddTransaction(transaction);
            }

            var t = _investmentPortfolio.Transactions;
        }
    }
}
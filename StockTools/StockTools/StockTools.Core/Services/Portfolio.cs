using StockTools.Core.Interfaces;
using StockTools.Core.Models;
using StockTools.Core.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace StockTools.Core.Services
{
    /// <summary>
    /// In the future this class should implement IPortfolio, but depend on abstractions
    /// </summary>
    [Serializable]
    public class Portfolio : IPortfolio
    {
        public Portfolio(Func<double, double> chargeFunction,
                         double cash)
        {
            ChargeFunction = chargeFunction;
            _cash = cash;
            _items = new List<InvestmentPortfolioItem>();
            _transactions = new List<Transaction>();
            _taxationList = new List<Taxation>();
        }

        #region Fields

        private double _cash;

        public double Cash
        {
            get { return _cash; }
        }

        private List<InvestmentPortfolioItem> _items;

        public List<InvestmentPortfolioItem> Items
        {
            get { return _items; }
        }

        private List<Transaction> _transactions;

        public List<Transaction> Transactions
        {
            get { return _transactions; }
        }

        public List<Taxation> TaxationList
        {
            set { _taxationList = value; }
        }

        public List<Dividend> Dividends { get; set; }

        public Func<double, double> ChargeFunction { get; set; }

        private List<Taxation> _taxationList;

        #endregion

        public void AddTransaction(Transaction transaction)
        {
            var companyExistsInPortfolio = _items.Any(x => x.CompanyName == transaction.CompanyName);
            var canBeSold = _items.Where(x => x.CompanyName == transaction.CompanyName)
                                  .Where(x => x.NumberOfShares >= transaction.Amount).ToList().Count != 0;

            if (transaction.TransactionType == Transaction.TransactionTypes.Buy)
            {
                if (companyExistsInPortfolio)
                {
                    _items.Where(x => x.CompanyName == transaction.CompanyName).Single().NumberOfShares += transaction.Amount;
                }

                if (_cash - transaction.Value - ChargeFunction(transaction.Value) < 0)
                {
                    throw new NotEnoughMoneyException(this);
                }
                _cash -= transaction.Value;
                _cash -= ChargeFunction(transaction.Value);

                if (!companyExistsInPortfolio)
                {
                    _items.Add(new InvestmentPortfolioItem()
                    {
                        CompanyName = transaction.CompanyName,
                        NumberOfShares = transaction.Amount
                    });
                }

                _transactions.Add(transaction);
            }
            else
            {
                if (canBeSold)
                {
                    if (companyExistsInPortfolio)
                    {
                        _items.Where(x => x.CompanyName == transaction.CompanyName).Single().NumberOfShares -= transaction.Amount;
                    }

                    _cash += transaction.Value;
                    _cash -= ChargeFunction(transaction.Value);
                    _transactions.Add(transaction);
                }
            }
        }
    }
}
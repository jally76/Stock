using StockTools.Core.Interfaces;
using StockTools.Core.Models;
using System;
using System.Collections.Generic;

namespace StockTools.Core.Services
{
    /// <summary>
    /// In the future this class should implement IPortfolio, but depend on abstractions
    /// </summary>
    public class Portfolio : IPortfolio
    {
        public Portfolio(ICurrentPriceProvider currentPriceProvider,
                         IHIstoricalPriceRepository historicalPriceRepository,
                         Func<double, double> chargeFunction,
                         double cash)
        {
            CurrentPriceProvider = currentPriceProvider;
            HistoricalPriceRepository = historicalPriceRepository;
            ChargeFunction = chargeFunction;
            _cash = cash;
        }

        #region Fields

        public ICurrentPriceProvider CurrentPriceProvider { get; set; }

        public IHIstoricalPriceRepository HistoricalPriceRepository { get; set; }

        private double _cash;

        public double Cash
        {
            get { return _cash; }
        }

        public double Value
        {
            get { throw new NotImplementedException(); }
        }

        private List<InvestmentPortfolioItem> _items;

        public List<InvestmentPortfolioItem> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        private List<Transaction> _transactions;

        public List<Transaction> Transactions
        {
            get { return _transactions; }
            set { _transactions = value; }
        }

        public List<Dividend> Dividends { get; set; }

        public Func<double, double> ChargeFunction { get; set; }

        private List<Taxation> _taxationList;

        public List<Taxation> TaxationList
        {
            set { _taxationList = value; }
        }

        #endregion

        public void AddTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
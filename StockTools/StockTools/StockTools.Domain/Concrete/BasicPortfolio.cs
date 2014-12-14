﻿using StockTools.BusinessLogic.Abstract;
using StockTools.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.BusinessLogic.Concrete
{
    public class BasicPortfolio : IPortfolio
    {
        #region Initialization

        public BasicPortfolio(IPriceProvider priceService, Func<double, double> chargeFunction)
        {
            _priceService = priceService;
            _chargeFunction = chargeFunction;
        }

        #endregion

        #region Properties

        private IPriceProvider _priceService;

        public IPriceProvider PriceService
        {
            get { return _priceService; }
            set { _priceService = value; }
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
            set
            {
                //TODO Modify items and cash or throw exception if there's no cash
                _transactions = value;
            }
        }

        Func<double, double> _chargeFunction;

        public Func<double, double> ChargeFunction
        {
            get { return _chargeFunction; }
            set { _chargeFunction = value; }
        }

        double _cash;

        public double Cash
        {
            get { return _cash; }
            set { _cash = value; }
        }

        List<Taxation> _taxationList;

        public List<Taxation> TaxationList
        {
            get { return _taxationList; }
            set { _taxationList = value; }
        }

        public double Value
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region Methods

        public double GetGrossProfit()
        {
            throw new NotImplementedException();
        }

        public double GetNetProfit()
        {
            throw new NotImplementedException();
        }

        public double GetRealisedGrossProfit()
        {
            #region Prerequisite check

            if (Transactions == null)
            {
                throw new Exception("Transactions has not been set");
            }
            if (ChargeFunction == null)
            {
                throw new Exception("Charge function has not been set");
            }

            #endregion

            var orderedTransactions = Transactions.OrderBy(x => x.Time).ToList();
            Dictionary<string, Queue<Transaction>> companyTransaction = new Dictionary<string, Queue<Transaction>>();

            for (int i = 0; i < orderedTransactions.Count; i++)
            {
                var transaction = orderedTransactions[i];

                if (companyTransaction.ContainsKey(transaction.CompanyName))
                {
                    companyTransaction[transaction.CompanyName].Enqueue(transaction);
                }
                else
                {
                    companyTransaction[transaction.CompanyName] = new Queue<Transaction>();
                    companyTransaction[transaction.CompanyName].Enqueue(transaction);
                }
            }

            double charges = 0.0;
            double earnedMoney = 0.0;

            foreach (var key in companyTransaction.Keys)
            {
                var buyAmount = companyTransaction[key]
                    .Where(x => x.TransactionType == Transaction.TransactionTypes.Buy)
                    .Sum(x => x.Amount);
                var sellAmount = companyTransaction[key]
                    .Where(x => x.TransactionType == Transaction.TransactionTypes.Sell)
                    .Sum(x => x.Amount);

                if (buyAmount == sellAmount)
                {
                    while (companyTransaction[key].Count > 0)
                    {
                        var transaction = companyTransaction[key].Dequeue();
                        if (transaction.TransactionType == Transaction.TransactionTypes.Sell)
                        {
                            earnedMoney += transaction.Value;
                            charges += ChargeFunction(transaction.Value);
                        }
                        if (transaction.TransactionType == Transaction.TransactionTypes.Buy)
                        {
                            earnedMoney -= transaction.Value;
                            charges += ChargeFunction(transaction.Value);
                        }
                    }
                }

                if (buyAmount > sellAmount)
                {
                    if (sellAmount == 0)
                        continue;
                    DateTime lastSellTime = companyTransaction[key]
                        .Where(x => x.TransactionType == Transaction.TransactionTypes.Sell)
                        .OrderByDescending(x => x.Time)
                        .ToList()[0].Time;
                    var buyAmountBeforeLastSell = companyTransaction[key]
                        .Where(x => x.TransactionType == Transaction.TransactionTypes.Buy)
                        .Where(x => x.Time <= lastSellTime)
                        .Sum(x => x.Amount);
                    var averageBuyPrice = companyTransaction[key]
                        .Where(x => x.TransactionType == Transaction.TransactionTypes.Buy)
                        .Where(x => x.Time <= lastSellTime)
                        .Sum(x => x.Value) / buyAmountBeforeLastSell;
                    var sellLimit = sellAmount;

                    while (sellLimit > 0)
                    {
                        var transaction = companyTransaction[key].Dequeue();
                        if (transaction.TransactionType == Transaction.TransactionTypes.Sell)
                        {
                            sellLimit -= transaction.Amount;
                            charges += ChargeFunction(transaction.Value);
                            earnedMoney += transaction.Value - averageBuyPrice * transaction.Amount;
                        }
                        else if (transaction.TransactionType == Transaction.TransactionTypes.Buy)
                        {
                            charges += ChargeFunction(transaction.Value);
                        }
                        if (companyTransaction[key].Count == 0)
                        {
                            break;
                        }
                    }
                }

                if (buyAmount < sellAmount)
                {
                    throw new Exception("Error in the transaction list, there are more sold papers than bought!");
                }
            }

            return earnedMoney - charges;
        }

        public double GetRealisedNetProfit()
        {
            throw new NotImplementedException();
        }

        public List<Transaction> GetPairedTransactions()
        {
            if (Transactions.Count == 0)
            {
                return new List<Transaction>();
            }

            var paired = new List<int>();
            for (int i = Transactions.Count - 1; i >= 0; i--)
            {
                if (paired.Any(x => x == i))
                {
                    continue;
                }
                var pair = GetTransactionPair(Transactions[i]);
                if (pair != null)
                {
                    paired.Add(Transactions.IndexOf(Transactions[i]));
                    paired.Add(Transactions.IndexOf(pair));
                }
            }

            var result = new List<Transaction>(paired.Count);
            for (int i = paired.Count - 1; i >= 0; i--)
            {
                result.Add(Transactions[paired[i]]);
            }

            return result;
        }

        public Transaction GetTransactionPair(Transaction transaction)
        {
            var index = Transactions.IndexOf(transaction);
            for (int i = index - 1; i >= 0; i--)
            {
                var item = Transactions[i];
                if (item.CompanyName == transaction.CompanyName && item.TransactionType == Transaction.TransactionTypes.Sell)
                {
                    return item;
                }
            }
            return null;
        }

        #endregion






    }
}
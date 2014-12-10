using StockTools.BusinessLogic.Abstract;
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

        public BasicPortfolio(IPriceProvider priceService)
        {
            _priceService = priceService;
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

        private List<MBankTransaction> _transactions;

        public List<MBankTransaction> Transactions
        {
            get { return _transactions; }
            set { _transactions = value; }
        }

        Func<double, double> _chargeFunction;

        public Func<double, double> ChargeFunction
        {
            get { return _chargeFunction; }
            set { _chargeFunction = value; }
        }

        #endregion

        #region Methods

        public double GetValue()
        {
            throw new NotImplementedException();
        }

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

            #region Finding companies which have been sold

            Dictionary<string, int> amount = new Dictionary<string, int>();

            var orderedTransactions = Transactions.OrderBy(x => x.Time).ToList();

            foreach (var transaction in orderedTransactions)
            {
                if (transaction.TransactionType == MBankTransaction.TransactionTypes.Buy)
                {
                    if (amount.ContainsKey(transaction.CompanyName))
                    {
                        amount[transaction.CompanyName] += transaction.Amount;
                    }
                    else
                    {
                        amount[transaction.CompanyName] = transaction.Amount;
                    }
                }
                else if (transaction.TransactionType == MBankTransaction.TransactionTypes.Sell)
                {
                    amount[transaction.CompanyName] -= transaction.Amount;
                }
            }

            var soldCompanies = amount.Where(x => x.Value == 0).ToList();

            #endregion

            #region Calculating charges

            var orderedTransactionsOfSoldCompanies = orderedTransactions.Where(x => soldCompanies.Any(y => y.Key == x.CompanyName)).OrderBy(x => x.Time).ToList();

            double charges = 0.0;
            double earnedMoney = 0.0;

            foreach (var item in orderedTransactionsOfSoldCompanies)
            {
                charges += ChargeFunction(item.TotalValue);
                if (item.TransactionType == MBankTransaction.TransactionTypes.Buy)
                {
                    earnedMoney -= item.TotalValue;
                }
                else if (item.TransactionType == MBankTransaction.TransactionTypes.Sell)
                {
                    earnedMoney += item.TotalValue;
                }
                System.Diagnostics.Debug.WriteLine(string.Format("Earned money: {0}", earnedMoney));
            }

            #endregion

            return earnedMoney - charges;
        }

        public double GetRealisedNetProfit()
        {
            throw new NotImplementedException();
        }

        public void SetTaxation(List<Entities.Taxation> taxationList)
        {
            throw new NotImplementedException();
        }

        public List<MBankTransaction> GetPairedTransactions()
        {
            if (Transactions.Count == 0)
            {
                return new List<MBankTransaction>();
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

            var result = new List<MBankTransaction>(paired.Count);
            for (int i = paired.Count - 1; i >= 0; i--)
            {
                result.Add(Transactions[paired[i]]);
            }

            return result;
        }

        public MBankTransaction GetTransactionPair(MBankTransaction transaction)
        {
            var index = Transactions.IndexOf(transaction);
            for (int i = index - 1; i >= 0; i--)
            {
                var item = Transactions[i];
                if (item.CompanyName == transaction.CompanyName && item.TransactionType == MBankTransaction.TransactionTypes.Sell)
                {
                    return item;
                }
            }
            return null;
        }

        #endregion
    }
}

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
    public class InvestmentPortfolio : IInvestmentPortfolio
    {
        #region Initialization

        public InvestmentPortfolio(IPriceService priceService)
        {
            _priceService = priceService;
        }

        #endregion

        #region Properties

        private IPriceService _priceService;

        public IPriceService PriceService
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

            var orderedTransactions = Transactions.OrderBy(x => x.DateAndTime).ToList();

            foreach (var transaction in orderedTransactions)
            {
                if (transaction.TransactionType == Transaction.TransactionTypes.Buy)
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
                else if (transaction.TransactionType == Transaction.TransactionTypes.Sell)
                {
                    amount[transaction.CompanyName] -= transaction.Amount;
                }
            }

            var soldCompanies = amount.Where(x => x.Value == 0).ToList();

            #endregion

            #region Calculating charges

            var orderedTransactionsOfSoldCompanies = orderedTransactions.Where(x => soldCompanies.Any(y => y.Key == x.CompanyName)).OrderBy(x => x.DateAndTime).ToList();

            double charges = 0.0;
            double earnedMoney = 0.0;

            foreach (var item in orderedTransactionsOfSoldCompanies)
            {
                charges += ChargeFunction(item.TotalValue);
                if (item.TransactionType == Transaction.TransactionTypes.Buy)
                {
                    earnedMoney -= item.TotalValue;
                }
                else if (item.TransactionType == Transaction.TransactionTypes.Sell)
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

        #endregion



    }
}

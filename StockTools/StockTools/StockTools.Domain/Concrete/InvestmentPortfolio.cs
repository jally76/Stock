using StockTools.BusinessLogic.Abstract;
using StockTools.Entities;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public double GetRealisedNetProfit()
        {
            throw new NotImplementedException();
        }

        public void SetChargeFunc(Func<double, double> chargeFunction)
        {
            throw new NotImplementedException();
        }

        public void SetTaxation(List<Entities.Taxation> taxationList)
        {
            throw new NotImplementedException();
        }

        public void LoadTransactions(List<Transaction> transactionList)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

using StockTools.BusinessLogic.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.BusinessLogic.Concrete
{
    public class InvestmentPortfolio : IInvestmentPortfolio
    {
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
    }
}

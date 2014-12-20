using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.Entities
{
    public class InvestmentPortfolioItem : ICloneable
    {
        public string CompanyName { get; set; }
        public int NumberOfShares { get; set; }
        //public double BuyPrice { get; set; }
        //public double? SellPrice { get; set; }
        //public DateTime BuyDate { get; set; }
        //public DateTime? SellDate { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}

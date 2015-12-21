using System;

namespace StockTools.Core.Models
{
    public class InvestmentPortfolioItem : ICloneable
    {
        public string CompanyName { get; set; }

        public int NumberOfShares { get; set; }

        public double BuyPrice { get; set; }

        public DateTime BuyDate { get; set; }

        public double? SellPrice { get; set; }

        public DateTime? SellDate { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
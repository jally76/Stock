using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.Entities
{
    public class InvestmentPortfolio
    {
        //TODO Inject stock price service

        /// <summary>
        /// Sum of current prices of all shares in the portfolio
        /// </summary>
        public double Value { get { throw new NotImplementedException(); } }

        /// <summary>
        /// Gross profit of the portfolio
        /// </summary>
        public double GrossProfit { get { throw new NotImplementedException(); } }

        /// <summary>
        /// Net profit of the portfolio
        /// </summary>
        public double NetProfit { get { throw new NotImplementedException(); } }

        /// <summary>
        /// Sets charge function which is necessary for profit calculation
        /// </summary>
        public Func<double, double> ChargeFunc { set { throw new NotImplementedException(); } }

        /// <summary>
        /// Sets value of capital gain tax (during the time because we assume that it can change)
        /// </summary>
        public List<Taxation> Taxation { set { throw new NotImplementedException(); } }

        public class Item
        {
            public string CompanyName { get; set; }
            public int NumberOfShares { get; set; }
            public double BuyPrice { get; set; }
            public double? SellPrice { get; set; }
            public DateTime BuyDate { get; set; }
            public DateTime? SellDate { get; set; }

            /// <summary>
            /// Current price of all shares of particular company
            /// </summary>
            public double GetValue { get { throw new NotImplementedException(); } }
        }
    }
}

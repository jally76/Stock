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
        public double GetValue { get { throw new NotImplementedException(); } }

        /// <summary>
        /// Gross profit of the portfolio
        /// </summary>
        public double GetGrossProfit { get { throw new NotImplementedException(); } }

        /// <summary>
        /// Net profit of the portfolio
        /// </summary>
        public double GetNetProfit { get { throw new NotImplementedException(); } }

        /// <summary>
        /// Sets charge function which is necessary for profit calculation
        /// </summary>
        public void SetChargeFunc { set { throw new NotImplementedException(); } }

        public class Item
        {
            public string CompanyName { get; set; }
            public int NumberOfShares { get; set; }

            /// <summary>
            /// Current price of all shares of particular company
            /// </summary>
            public double GetValue { get { throw new NotImplementedException(); } }
        }
    }
}

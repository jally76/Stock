using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.Entities
{
    public class Transaction
    {
        public DateTime DateAndTime { get; set; }
        public string CompanyName { get; set; }
        public string TransactionType { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public double TotalValue { get; set; }
    }
}

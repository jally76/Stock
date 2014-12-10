using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.Entities
{
    public class MBankTransaction : Transaction
    {
        public string TransactionName { get; set; }
        public double TotalValue { get; set; }
    }
}

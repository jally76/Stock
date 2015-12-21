using System;

namespace StockTools.Core.Models
{
    public class Dividend
    {
        public Guid CompanyId { get; set; }

        public string CompanyName { get; set; }

        public double Value { get; set; }

        public DateTime Day { get; set; }
    }
}
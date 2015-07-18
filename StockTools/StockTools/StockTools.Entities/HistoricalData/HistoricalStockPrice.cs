using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.Entities.HistoricalData
{
    public class HistoricalStockPrice
    {
        //Metastock prn format:
        //Name, Date (yyyymmdd), Hour (hhmmss), Open, High, Low, Close, Volume
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Volume { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.Entities
{
    /// <summary>
    /// Taxation in a period of a time. Value means percentage of taxation.
    /// </summary>
    public class Taxation
    {
        public double Value { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}

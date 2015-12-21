using System;

namespace StockTools.Core.Models
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
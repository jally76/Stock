﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.Entities
{
    public class Order
    {
        //TODO Complete to full
        public string CompanyName { get; set; }
        public double PriceLimit { get; set; }
        public double? ActivationLimit { get; set; }
        public bool AnyPrice { get; set; } //PKC
        public bool MarketToLimit { get; set; } //PCR
        public bool PEG { get; set; }
        
    }
}

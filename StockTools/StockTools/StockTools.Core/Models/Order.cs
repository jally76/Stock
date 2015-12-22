﻿namespace StockTools.Core.Models
{
    public class Order
    {
        //TODO Complete to full
        public enum OrderTypes { Buy, Sell }

        public OrderTypes OrderType { get; set; }

        public string CompanyName { get; set; }

        public double PriceLimit { get; set; }

        public int Amount { get; set; }

        public double? ActivationLimit { get; set; }

        public bool AnyPrice { get; set; } //PKC

        public bool MarketToLimit { get; set; } //PCR

        public bool PEG { get; set; }
    }
}
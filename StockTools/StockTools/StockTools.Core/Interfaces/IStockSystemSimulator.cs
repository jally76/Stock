using StockTools.Core.Models;
using System;

namespace StockTools.Core.Interfaces
{
    public interface IStockSystemSimulator
    {
        public DateTime CurrentDate { get; }

        public bool IsStockMarketAvailable { get; }

        async Transaction SubmitOrder(Order order);

        void Tick(TimeSpan timeSpan);
    }
}
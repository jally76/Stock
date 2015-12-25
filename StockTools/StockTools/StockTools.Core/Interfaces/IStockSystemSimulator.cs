using StockTools.Core.Models;
using StockTools.Core.Models.Delegates;
using System;

namespace StockTools.Core.Interfaces
{
    public interface IStockSystemSimulator
    {
        DateTime CurrentDate { get; }

        bool IsStockMarketAvailable { get; }

        void SubmitOrder(Order order);

        event OrderProcessedDelegate OrderProcessed;

        void Tick(TimeSpan timeSpan);
    }
}
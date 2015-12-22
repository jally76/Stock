using StockTools.Core.Models;
using System;
using System.Threading.Tasks;

namespace StockTools.Core.Interfaces
{
    public interface IStockSystemSimulator
    {
        DateTime CurrentDate { get; }

        bool IsStockMarketAvailable { get; }

        Transaction SubmitOrder(Order order, IPortfolio portfolio);

        void Tick(TimeSpan timeSpan);
    }
}
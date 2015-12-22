using StockTools.Core.Models;
using System;
using System.Threading.Tasks;

namespace StockTools.Core.Interfaces
{
    public interface IStockSystemSimulator
    {
        DateTime CurrentDate { get; }

        bool IsStockMarketAvailable { get; }

        Task<Transaction> SubmitOrder(Order order);

        void Tick(TimeSpan timeSpan);
    }
}
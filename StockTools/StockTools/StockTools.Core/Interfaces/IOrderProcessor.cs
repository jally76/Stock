using StockTools.Core.Models;
using StockTools.Core.Models.Delegates;
using System;

namespace StockTools.Core.Interfaces
{
    public interface IOrderProcessor
    {
        void Enqueue(Order order);

        void OnCurrentDateChanged(object sender, EventArgs e);

        event OrderProcessedDelegate OrderProcessed;
    }
}
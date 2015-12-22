using StockTools.Core.Models;
using System.Collections.Generic;

namespace StockTools.Core.Interfaces
{
    public interface IStrategy
    {
        List<Order> GenerateOrders();
    }
}
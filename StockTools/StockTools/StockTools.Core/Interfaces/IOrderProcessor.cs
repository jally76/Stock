using StockTools.Core.Models;

namespace StockTools.Core.Interfaces
{
    public interface IOrderProcessor
    {
        Transaction ProcessOrder(Order order);
    }
}
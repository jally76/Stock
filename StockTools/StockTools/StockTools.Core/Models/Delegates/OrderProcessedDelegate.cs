using StockTools.Core.Models.EventArgs;

namespace StockTools.Core.Models.Delegates
{
    public delegate void OrderProcessedDelegate(object sender, OrderEventArgs eventArgs);
}
namespace StockTools.Core.Models.EventArgs
{
    public class OrderEventArgs : System.EventArgs
    {
        public OrderEventArgs(Transaction transaction)
        {
            Transaction = transaction;
        }

        public Transaction Transaction { get; set; }
    }
}
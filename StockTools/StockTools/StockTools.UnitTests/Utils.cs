using System;

namespace StockTools.UnitTests
{
    public class Utils
    {
        private static readonly Lazy<Utils> lazy = new Lazy<Utils>(() => new Utils());

        public static Utils Instance { get { return lazy.Value; } }

        private Utils()
        {
        }

        public double ChargeFunc(double price)
        {
            if (price <= 769)
            {
                return 3.0;
            }
            else
            {
                return price * (0.39 / 100.0);
            }
        }
    }
}
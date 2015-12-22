using StockTools.Core.Interfaces;
using System;

namespace StockTools.Core.Models.Exceptions
{
    public class NotEnoughMoneyException : Exception
    {
        public NotEnoughMoneyException(IPortfolio portfolio)
        {
            this.Data["Portfolio"] = portfolio;
        }
    }
}
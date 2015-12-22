using StockTools.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace StockTools.Infrastructure.Data
{
    public class HistoricalPriceRepository : IHistoricalPriceRepository
    {
        public double Get(string companyName, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public double GetClosest(string companyName, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public Dictionary<DateTime, double> GetAll(string companyName, DateTime day)
        {
            throw new NotImplementedException();
        }
    }
}
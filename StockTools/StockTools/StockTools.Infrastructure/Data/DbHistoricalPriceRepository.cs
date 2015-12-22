﻿using StockTools.Core.Interfaces;
using StockTools.Data.HistoricalData;
using System;
using System.Collections.Generic;

namespace StockTools.Infrastructure.Data
{
    public class DbHistoricalPriceRepository : IHistoricalPriceRepository
    {
        #region DI

        private IDbHistoricalDataProvider _dbHistoricalDataProvider;

        public IDbHistoricalDataProvider DbHistoricalDataProvider
        {
            get { return _dbHistoricalDataProvider; }
            set { _dbHistoricalDataProvider = value; }
        }

        #endregion DI

        public DbHistoricalPriceRepository(IDbHistoricalDataProvider dbHistoricalDataProvider)
        {
            _dbHistoricalDataProvider = dbHistoricalDataProvider;
        }

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

        public bool AnyTradingInDay(DateTime dateTime)
        {
            return _dbHistoricalDataProvider.AnyTradingInDay(dateTime);
        }

        public bool IsThereCompany(string companyName, DateTime dateTime)
        {
            return _dbHistoricalDataProvider.IsThereCompany(companyName, dateTime);
        }
    }
}
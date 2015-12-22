﻿using System;
using System.Collections.Generic;

namespace StockTools.Core.Interfaces
{
    public interface IHistoricalPriceRepository
    {
        /// <summary>
        /// Gets historical price
        /// </summary>
        /// <param name="companyName">Full company name (for instance CIECH)</param>
        /// <param name="dateTime">Date time</param>
        /// <returns></returns>
        double Get(string companyName, DateTime dateTime);

        /// <summary>
        /// Gets historical price which is closest to specified datetime stamp.
        /// For instance if dateTime = 2015.02.15 09:10 and prices were on 9:08 and 9:11
        /// method will return price of 9:11.
        /// </summary>
        /// <param name="companyName">Full company name (for instance CIECH)</param>
        /// <param name="dateTime">Date time</param>
        /// <returns>Price</returns>
        double GetClosest(string companyName, DateTime dateTime);

        /// <summary>
        /// Gets list of prices with timestamps in a given day
        /// </summary>
        /// <param name="companyName">Full company name (for instance CIECH)</param>
        /// <param name="day">Year month and day</param>
        /// <returns></returns>
        Dictionary<DateTime, double> GetAll(string companyName, DateTime day);

        /// <summary>
        /// True if at least one trading appeared on given day
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>True if at least one trading appeared on given day</returns>
        bool AnyTradingInDay(DateTime dateTime);
    }
}
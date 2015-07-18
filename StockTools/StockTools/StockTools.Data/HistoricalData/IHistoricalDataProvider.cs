using StockTools.Entities.HistoricalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.Data.HistoricalData
{
    public interface IHistoricalDataProvider
    {
        Price GetSingle(string name, DateTime dateTime);
        void AddSingle(Price price);
        List<Price> GetListByDay(string name, DateTime dateTime);
    }
}

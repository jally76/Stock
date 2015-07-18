using StockTools.Data.HistoricalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.Converters.MetastockToDb
{
    public interface IMetastockToDbConverter
    {
        IHistoricalDataProvider HistoricalDataProvider { get; set; }
        void InsertIntradayFileDataToDatabase(string path);
    }
}

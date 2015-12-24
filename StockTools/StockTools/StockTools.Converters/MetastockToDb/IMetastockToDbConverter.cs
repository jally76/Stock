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
        IDbHistoricalDataProvider DbHistoricalDataProvider { get; set; }
        void InsertIntradayFileDataToDatabase(string path);

        void InsertIntradayFileDataToDatabaseQuick(string path);
    }
}

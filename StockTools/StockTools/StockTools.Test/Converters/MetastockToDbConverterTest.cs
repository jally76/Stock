using StockTools.Converters;
using StockTools.Converters.MetastockToDb;
using StockTools.Data.HistoricalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StockTools.Test.Converters
{
    public class MetastockToDbConverterTest
    {
        //InsertIntradayFileDataToDatabase

        [Fact]
        void MetastockToDbConverterTest_InsertIntradayFileDataToDatabase()
        {
            StockEntities dbContext = new StockEntities();
            IHistoricalDataProvider hdp = new HistoricalDataProvider(dbContext);
            IMetastockToDbConverter converter = new MetastockToDbConverter();
            converter.HistoricalDataProvider = hdp;
            var path = Environment.CurrentDirectory + "\\..\\..\\..\\IntradayData\\2014-12-08-tick\\a_cgl.prn";
            converter.InsertIntradayFileDataToDatabase(path);
        }
    }
}

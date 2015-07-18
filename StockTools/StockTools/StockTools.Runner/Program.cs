using StockTools.Converters;
using StockTools.Converters.MetastockToDb;
using StockTools.Data.HistoricalData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(":: Stock tools console runner");
            Console.WriteLine("");
            Console.WriteLine("Select action:");
            Console.WriteLine("");
            Console.WriteLine("a) Convert all metastock files into database entries");
            var key = Console.ReadLine();
            if (key == "a")
            {
                //var path = Environment.CurrentDirectory + "\\..\\..\\..\\IntradayData";
                var path = "C:\\IntradayData";
                var files = Directory.GetFiles(path, "*.prn", SearchOption.AllDirectories);
                int counter = 0;
                int count = files.Count();
                StockEntities dbContext = new StockEntities();
                IHistoricalDataProvider hdp = new HistoricalDataProvider(dbContext);
                IMetastockToDbConverter converter = new MetastockToDbConverter();
                converter.HistoricalDataProvider = hdp;
                foreach (var file in files)
                {
                    counter++;
                    if (counter % 5 == 0)
                    {
                        Console.WriteLine("{0}%", ((double)counter / count) * 100);
                    }
                    converter.InsertIntradayFileDataToDatabaseQuick(file);
                    try
                    {
                        //File.Delete(file);
                        Directory.Delete(Path.GetDirectoryName(file), true);
                    }
                    catch
                    {
                        //Just fuck it.
                    }
                }
            }
            else
            {
                Console.WriteLine("There's no such command");
            }
            Console.WriteLine("Runner has finished.");
            Console.ReadKey();
        }
    }
}

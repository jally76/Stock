using StockTools.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.Domain.Concrete
{
    public class BOSSAArchivePriceProvider : IArchivePriceProvider
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="path">Path to intraday price files</param>
        public BOSSAArchivePriceProvider(string path)
        {
            _data = new Dictionary<string, Dictionary<DateTime, IntradayInfo>>();
            _path = path;
        }

        public class IntradayInfo
        {
            public string CompanyName { get; set; }
            public int SomeField { get; set; } //Not sure
            public DateTime DateAndTime { get; set; }
            public double PriceOpen { get; set; }
            public double PriceHigh { get; set; }
            public double PriceLow { get; set; }
            public double PriceClose { get; set; }
            public int Volume { get; set; }
            public int OpenInterests { get; set; } //Not sure
        }

        //This structure is lazy loaded
        Dictionary<string, Dictionary<DateTime, IntradayInfo>> _data;
        string _path;

        private void LoadDay(DateTime date)
        {
            var fileName = string.Format("{0}{1}-{2}-{3}-tick\\a_cgl.prn", _path, date.ToString("yyyy"), date.ToString("MM"), date.ToString("dd"));
            if (!File.Exists(fileName))
            {
                return;
            }
            
            var lines = File.ReadAllLines(fileName, Encoding.UTF8);

            foreach (var line in lines)
            {
                var splittedLine = line.Split(',');

                var price = new IntradayInfo
                {
                    CompanyName = splittedLine[0],
                    SomeField = Convert.ToInt32(splittedLine[1]),
                    //DateAndTime = Convert.ToDateTime(splittedLine[2]),
                    PriceOpen = Convert.ToDouble(splittedLine[4].Replace('.', ',')),
                    PriceHigh = Convert.ToDouble(splittedLine[5].Replace('.', ',')),
                    PriceLow = Convert.ToDouble(splittedLine[6].Replace('.', ',')),
                    PriceClose = Convert.ToDouble(splittedLine[7].Replace('.', ',')),
                    Volume = Convert.ToInt32(splittedLine[8]),
                    OpenInterests = Convert.ToInt32(splittedLine[9]),
                };
                price.DateAndTime = DateTime.ParseExact(splittedLine[2] + splittedLine[3], "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                if (!_data.ContainsKey(price.CompanyName))
                {
                    _data[price.CompanyName] = new Dictionary<DateTime, IntradayInfo>();
                }
                _data[price.CompanyName][price.DateAndTime] = price;
            }
        }

        public double? GetPriceByFullNameAndDateTime(string shortName, DateTime dateTime)
        {
            if (_data.ContainsKey(shortName))
            {
                if (_data[shortName].ContainsKey(dateTime))
                {
                    return _data[shortName][dateTime].PriceClose;
                }
            }

            LoadDay(dateTime);

            if (_data.ContainsKey(shortName))
            {
                if (_data[shortName].ContainsKey(dateTime))
                {
                    //Return exact
                    return _data[shortName][dateTime].PriceClose;
                }
                else
                {
                    //Return next tick
                    var keys = new List<DateTime>(_data[shortName].Keys.OrderBy(x => x));
                    var index = 0 - keys.BinarySearch(dateTime);
                    var closestDate = keys[index - 1];
                    return _data[shortName][closestDate].PriceClose;
                }
            }

            return null;
        }
    }
}

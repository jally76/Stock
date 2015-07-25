using AutoMapper;
using StockTools.Converters.MetastockToDb;
using StockTools.Data.HistoricalData;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.Converters
{
    public class MetastockToDbConverter : IMetastockToDbConverter
    {
        public string Path { get; set; }
        Dictionary<string, Dictionary<DateTime, IntradayInfo>> Data;
        public IHistoricalDataProvider HistoricalDataProvider { get; set; }

        public class IntradayInfo
        {
            public string CompanyName { get; set; }
            public DateTime DateAndTime { get; set; }
            public double PriceClose { get; set; }
            public int Volume { get; set; }
        }

        private void LoadDay()
        {
            var fileName = Path;
            if (!File.Exists(fileName))
            {
                return;
            }

            var lines = File.ReadAllLines(fileName, Encoding.UTF8);
            Data = new Dictionary<string, Dictionary<DateTime, IntradayInfo>>();

            foreach (var line in lines)
            {
                var splittedLine = line.Split(',');

                var price = new IntradayInfo
                {
                    CompanyName = splittedLine[0],
                    PriceClose = Convert.ToDouble(splittedLine[7].Replace('.', ',')),
                    Volume = Convert.ToInt32(splittedLine[8]),
                };
                price.DateAndTime = DateTime.ParseExact(splittedLine[2] + splittedLine[3], "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                if (!Data.ContainsKey(price.CompanyName))
                {
                    Data[price.CompanyName] = new Dictionary<DateTime, IntradayInfo>();
                }
                Data[price.CompanyName][price.DateAndTime] = price;
            }
        }

        public void InsertIntradayFileDataToDatabase(string path)
        {
            Path = path;
            LoadDay();

            foreach (var name in Data.Keys)
            {
                foreach (var date in Data[name].Keys)
                {
                    IntradayInfo info = Data[name][date];
                    var company = HistoricalDataProvider.GetCompany(info.CompanyName);
                    if (company == null)
                    {
                        HistoricalDataProvider.AddCompany(new Company()
                        {
                            Name = info.CompanyName
                        });
                        HistoricalDataProvider.Save();
                        company = HistoricalDataProvider.GetCompany(info.CompanyName);
                    }

                    var dbInfo = new Price();
                    dbInfo.Close = info.PriceClose;
                    dbInfo.DateTime = info.DateAndTime;
                    dbInfo.Volume = info.Volume;
                    dbInfo.CompanyId = company.Id;
                    HistoricalDataProvider.AddPrice(dbInfo);
                    HistoricalDataProvider.Save();
                }
            }
        }

        public Dictionary<string, int> ExistingCompanies { get; set; }

        public void InsertIntradayFileDataToDatabaseQuick(string path)
        {
            Path = path;

            var fileName = Path;
            if (!File.Exists(fileName))
            {
                return;
            }

            var lines = File.ReadAllLines(fileName, Encoding.UTF8);
            if (ExistingCompanies == null)
            {
                ExistingCompanies = new Dictionary<string, int>();
                //Massive loading prices to reduce multiple db query overhead if companies are already in db
                var companiesTemporary = HistoricalDataProvider.GetCompanies();
                companiesTemporary.Each(x => ExistingCompanies[x.Name.ToUpperInvariant()] = x.Id);
            }

            var pricesToAdd = new List<Price>();

            foreach (var line in lines)
            {
                var splittedLine = line.Split(',');

                var price = new Price
                {
                    Close = Convert.ToDouble(splittedLine[7].Replace('.', ',')),
                    Volume = Convert.ToInt32(splittedLine[8]),
                };
                price.DateTime = DateTime.ParseExact(splittedLine[2] + splittedLine[3], "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                var companyName = splittedLine[0].ToUpperInvariant();
                if (!ExistingCompanies.ContainsKey(companyName))
                {
                    var company = HistoricalDataProvider.GetCompany(companyName);

                    if (company == null)
                    {
                        HistoricalDataProvider.AddCompany(new Company()
                        {
                            Name = companyName
                        });
                        HistoricalDataProvider.Save();
                        company = HistoricalDataProvider.GetCompany(companyName);
                        ExistingCompanies[company.Name] = company.Id;
                    }
                    else
                    {
                        ExistingCompanies[company.Name] = company.Id;
                    }
                }
                price.CompanyId = ExistingCompanies[companyName];

                //HistoricalDataProvider.AddPrice(price);
                pricesToAdd.Add(price);
            }
            //pricesToAdd.Each(x => HistoricalDataProvider.AddPrice(x));
            HistoricalDataProvider.AddRangePrice(pricesToAdd);
            HistoricalDataProvider.Save();
        }
    }
}

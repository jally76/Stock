using AutoMapper;
using StockTools.Converters.MetastockToDb;
using StockTools.Data.HistoricalData;
using StockTools.Data.SQL;
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
        public IDbHistoricalDataProvider DbHistoricalDataProvider { get; set; }

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
                    var company = DbHistoricalDataProvider.GetCompany(info.CompanyName);
                    if (company == null)
                    {
                        DbHistoricalDataProvider.AddCompany(new Company()
                        {
                            Name = info.CompanyName
                        });
                        DbHistoricalDataProvider.Save();
                        company = DbHistoricalDataProvider.GetCompany(info.CompanyName);
                    }

                    var dbInfo = new Price();
                    dbInfo.Close = info.PriceClose;
                    dbInfo.DateTime = info.DateAndTime;
                    dbInfo.Volume = info.Volume;
                    dbInfo.CompanyId = company.Id;
                    DbHistoricalDataProvider.AddPrice(dbInfo);
                    DbHistoricalDataProvider.Save();
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
                var companiesTemporary = DbHistoricalDataProvider.GetCompanies();
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
                    var company = DbHistoricalDataProvider.GetCompany(companyName);

                    if (company == null)
                    {
                        DbHistoricalDataProvider.AddCompany(new Company()
                        {
                            Name = companyName
                        });
                        DbHistoricalDataProvider.Save();
                        company = DbHistoricalDataProvider.GetCompany(companyName);
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
            DbHistoricalDataProvider.AddRangePrice(pricesToAdd);
            DbHistoricalDataProvider.Save();
        }
    }
}

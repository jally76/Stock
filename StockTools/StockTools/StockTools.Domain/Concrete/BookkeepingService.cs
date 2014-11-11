using StockTools.Domain.Abstract;
using StockTools.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace StockTools.Domain.Concrete
{
    public class BookkeepingService : IBookkeepingService
    {
        public List<Transaction> ReadTransactionHistory(MemoryStream stream)
        {
            StreamReader reader = new StreamReader(stream,Encoding.Unicode);
            var result = new List<Transaction>();
            string line;

            do
            {
                line = reader.ReadLine();
                if (line == null)
                {
                    break;
                }
                var split = line.Split(';');
                split[4] = split[4].Replace('.',',');
                result.Add(new Transaction()
                {
                    DateAndTime = DateTime.ParseExact(
                        split[0],
                        "yyyy-MM-dd-HH.mm.ss",
                        CultureInfo.InvariantCulture),
                    CompanyName = split[1],
                    TransactionType = split[2],
                    Amount = Convert.ToInt32(split[3]),
                    Price = Convert.ToDouble(split[4]),
                    TotalValue = Convert.ToDouble(split[5])
                });

            } while (true);

            return result;
        }

        public InvestmentPortfolio BuildInvestmentPortfolio(List<Transaction> transactions)
        {
            throw new NotImplementedException();
        }
    }
}

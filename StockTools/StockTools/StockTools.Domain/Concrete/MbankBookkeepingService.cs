using StockTools.BusinessLogic.Concrete;
using StockTools.Domain.Abstract;
using StockTools.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace StockTools.Domain.Concrete
{
    public class MbankBookkeepingService : IBookkeepingService
    {
        public List<Transaction> ReadTransactionHistory(MemoryStream stream)
        {
            StreamReader reader = new StreamReader(stream, Encoding.Unicode);
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
                split[4] = split[4].Replace('.', ',');
                Transaction.TransactionTypes? transactionType = null;
                if (split[2] == "KUPNO")
                {
                    transactionType = Transaction.TransactionTypes.Buy;
                }
                else if (split[2] == "SPRZEDAŻ")
                {
                    transactionType = Transaction.TransactionTypes.Sell;
                }
                result.Add(new Transaction()
                {
                    DateAndTime = DateTime.ParseExact(
                        split[0],
                        "yyyy-MM-dd-HH.mm.ss",
                        CultureInfo.InvariantCulture),
                    CompanyName = split[1],
                    TransactionName = split[2],
                    TransactionType = transactionType.Value,
                    Amount = Convert.ToInt32(split[3]),
                    Price = Convert.ToDouble(split[4]),
                    TotalValue = Convert.ToDouble(split[5])
                });

            } while (true);

            return result;
        }
    }
}

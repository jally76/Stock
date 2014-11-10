using StockTools.BusinessLogic.Abstract;
using StockTools.BusinessLogic.Concrete;
using StockTools.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace StockTools.Test
{
    public class BookkeepingServiceTest
    {
        //List<Transaction> ReadTransactionHistory(MemoryStream stream);

        [Fact]
        public void BookkeepingService_ReadTransactionHistory()
        {
            #region Arrange

            IBookkeepingService _bookkeepingService = new BookkeepingService();
            var path = Environment.CurrentDirectory + "\\Files\\transactions.csv";
            var file = File.ReadAllBytes(path);
            MemoryStream stream = new MemoryStream(file);

            var expectedResult = new List<Transaction>(4);
            expectedResult.Add(new Transaction()
            {
                DateAndTime = new DateTime(2014, 7, 9, 9, 29, 10),
                CompanyName = "MBANK",
                TransactionType = "SPRZEDAŻ",
                Amount = 1,
                Price = 485.05,
                TotalValue = 485.05
            });
            expectedResult.Add(new Transaction()
            {
                DateAndTime = new DateTime(2014, 7, 7, 14, 5, 0),
                CompanyName = "MILLENNIUM",
                TransactionType = "KUPNO",
                Amount = 100,
                Price = 8.05,
                TotalValue = 805.0
            });
            expectedResult.Add(new Transaction()
            {
                DateAndTime = new DateTime(2014, 7, 7, 9, 20, 0),
                CompanyName = "CORMAY",
                TransactionType = "KUPNO",
                Amount = 100,
                Price = 5.47,
                TotalValue = 547.0
            });
            expectedResult.Add(new Transaction()
            {
                DateAndTime = new DateTime(2014, 7, 4, 9, 18, 00),
                CompanyName = "MBANK",
                TransactionType = "KUPNO",
                Amount = 1,
                Price = 500.0,
                TotalValue = 500.0
            });

            #endregion

            #region Act

            var result = _bookkeepingService.ReadTransactionHistory(stream);

            #endregion

            #region Assert

            Assert.NotEmpty(result);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.True(Helper.ArePropertiesEqual(result[i], expectedResult[i]));
            }

            #endregion
        }
    }
}

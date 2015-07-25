using Moq;
using StockTools.Domain.Abstract;
using StockTools.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StockTools.Test
{
    public class TransactionTemporaryCalc
    {
        private double ChargeFunc(double price)
        {
            if (price <= 769)
            {
                return 3.0;
            }
            else
            {
                return price * (0.39 / 100.0);
            }
        }

        [Fact]
        void Calculate()
        {
            #region Arrange


            IBookkeepingService _bookkeepingService = new MbankBookkeepingService();
            var path = Environment.CurrentDirectory + "\\Files\\transactions20141114.csv";
            var file = File.ReadAllBytes(path);
            MemoryStream stream = new MemoryStream(file);

            Mock<IPriceProvider> archivePriceProviderMock = new Mock<IPriceProvider>();
            archivePriceProviderMock.Setup(x => x.GetPrice(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1.0);

            IPortfolio _investmentPortfolio = new Portfolio(archivePriceProviderMock.Object, ChargeFunc);

            #endregion


            #region Act

            var transactions = _bookkeepingService.ReadTransactionHistory(stream);
            foreach (var transaction in transactions)
            {
                _investmentPortfolio.AddTransaction(transaction);
            }
            //_investmentPortfolio.Transactions = transactions;
            var result = _investmentPortfolio.GetRealisedGrossProfit();

            #endregion

        }
    }
}

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

            Mock<ICurrentPriceProvider> currentPriceProviderMock = new Mock<ICurrentPriceProvider>();
            currentPriceProviderMock.Setup(x => x.GetPriceByFullName(It.IsAny<string>())).Returns(1.0);
            currentPriceProviderMock.Setup(x => x.GetPriceByShortName(It.IsAny<string>())).Returns(1.0);

            Mock<IArchivePriceProvider> archivePriceProviderMock = new Mock<IArchivePriceProvider>();
            archivePriceProviderMock.Setup(x => x.GetPriceByFullNameAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1.0);

            IPortfolio _investmentPortfolio = new BasicPortfolio(currentPriceProviderMock.Object, archivePriceProviderMock.Object, ChargeFunc);

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

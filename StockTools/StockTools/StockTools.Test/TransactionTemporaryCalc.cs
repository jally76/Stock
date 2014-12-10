using Moq;
using StockTools.BusinessLogic.Abstract;
using StockTools.BusinessLogic.Concrete;
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

            Mock<IPriceProvider> mock = new Mock<IPriceProvider>();
            mock.Setup(x => x.GetPriceByFullName(It.IsAny<string>())).Returns(1.0);
            mock.Setup(x => x.GetPriceByFullNameAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1.0);
            mock.Setup(x => x.GetPriceByShortName(It.IsAny<string>())).Returns(1.0);
            mock.Setup(x => x.GetPriceByShortNameAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1.0);

            IPortfolio _investmentPortfolio = new BasicPortfolio(mock.Object, ChargeFunc);

            #endregion


            #region Act

            var transactions = _bookkeepingService.ReadTransactionHistory(stream);
            _investmentPortfolio.Transactions = transactions;
            var result = _investmentPortfolio.GetRealisedGrossProfit();

            #endregion

        }
    }
}

using StockTools.BusinessLogic.Abstract;
using StockTools.BusinessLogic.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StockTools.Test
{
    public class BOSSAArchivePriceProviderTest
    {
        [Fact]
        public void BOSSAArchivePriceProvider_GetPriceByFullNameAndDateTime_Simple()
        {
            #region Arrange

            var path = Environment.CurrentDirectory + "\\Files\\BOSSA\\Intraday\\";
            IArchivePriceProvider provider = new BOSSAArchivePriceProvider(path);

            var expectedResult1 = 200.05;
            var expectedResult2 = 203.60;
            var expectedResult3 = 20.15;

            #endregion

            #region Act

            var result1 = provider.GetPriceByFullNameAndDateTime("MEDICALG", new DateTime(2014, 12, 8, 15, 38, 36));
            var result2 = provider.GetPriceByFullNameAndDateTime("MEDICALG", new DateTime(2014, 12, 8, 9, 0, 48));
            var result3 = provider.GetPriceByFullNameAndDateTime("JSW", new DateTime(2014, 12, 8, 9, 22, 14));

            #endregion

            #region Assert

            Assert.Equal(expectedResult1, result1);
            Assert.Equal(expectedResult2, result2);
            Assert.Equal(expectedResult3, result3);

            #endregion
        }

        [Fact]
        public void BOSSAArchivePriceProvider_GetPriceByFullNameAndDateTime_ClosestPrice()
        {
            #region Arrange

            var path = Environment.CurrentDirectory + "\\Files\\BOSSA\\Intraday\\";
            IArchivePriceProvider provider = new BOSSAArchivePriceProvider(path);

            var expectedResult1 = 200.05;
            var expectedResult2 = 202.50;
            var expectedResult3 = 20.19;

            #endregion

            #region Act

            var result1 = provider.GetPriceByFullNameAndDateTime("MEDICALG", new DateTime(2014, 12, 8, 15, 38, 35));
            var result2 = provider.GetPriceByFullNameAndDateTime("MEDICALG", new DateTime(2014, 12, 8, 9, 0, 55));
            var result3 = provider.GetPriceByFullNameAndDateTime("JSW", new DateTime(2014, 12, 8, 9, 23, 14));

            #endregion

            #region Assert

            Assert.Equal(expectedResult1, result1);
            Assert.Equal(expectedResult2, result2);
            Assert.Equal(expectedResult3, result3);

            #endregion
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Net.Http;
using StockTools.Domain.Abstract;
using StockTools.Domain.Concrete;

namespace StockTools.Test
{
    public class BOSSAIntradayDataDownloaderTest
    {
        string address = "http://bossa.pl/index.jsp?layout=intraday&page=1&news_cat_id=875&dirpath=/mstock/daily/";

        [Fact]
        public void BOSSA_Intraday_Connection()
        {
            var result = new HttpClient().GetAsync(address).Result.Content.ReadAsStringAsync().Result;
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void BOSSA_Intraday_Parse()
        {
            #region Arrange
            //IIntradayDataDownloader _intradayDataDownloader = new BOSSAIntradayDataDownloader();
            IIntradayDataParser _intradayDataParser = new BOSSAIntradayDataParser();

            #endregion

            #region Act

            var result = _intradayDataParser.GetFileAddresses(address);

            #endregion

            #region Assert

            Assert.True(result["2000-11-17-tick.zip"] == @"http://bossa.pl/pub/intraday/mstock/daily//2000-11-17-tick.zip");

            #endregion
        }
    }
}

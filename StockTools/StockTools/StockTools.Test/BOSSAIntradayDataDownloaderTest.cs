using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Net.Http;

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
    }
}

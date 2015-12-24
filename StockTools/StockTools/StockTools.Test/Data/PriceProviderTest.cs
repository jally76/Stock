using Moq;
using StockTools.Data.HistoricalData;
using StockTools.Data.SQL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StockTools.Test.Data
{
    public class PriceProviderTest
    {
        public StockEntities DbContext { get; set; }
        public IDbHistoricalDataProvider DbHistoricalDataProvider { get; set; }
        public PriceProviderTest()
        {
            InitDbContext();
            DbHistoricalDataProvider = new SQLHistoricalDataProvider(DbContext);
        }

        [Fact]
        void DbMockTest()
        {
            var price = DbContext.Prices.Where(x => x.Id == 1).SingleOrDefault().Close;
            Assert.Equal(15, price);
        }

        void InitDbContext()
        {
            //https://msdn.microsoft.com/en-us/data/dn314429 - mocking dbcontext in ef 6
            var data = new List<Price> 
            { 
                new Price 
                { 
                    Id=1,
                    Close= 15
                }, 
                new Price 
                { 
                    Id=2
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Price>>();
            mockSet.As<IQueryable<Price>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Price>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Price>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Price>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator()); 

            var mockContext = new Mock<StockEntities>();
            mockContext.Setup(m => m.Prices).Returns(mockSet.Object);
            
            DbContext = mockContext.Object;
        }
    }
}

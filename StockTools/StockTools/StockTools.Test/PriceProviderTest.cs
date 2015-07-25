using Moq;
using StockTools.BusinessLogic.Concrete;
using StockTools.Data.HistoricalData;
using StockTools.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StockTools.Test
{
    public class PriceProviderTest
    {
        public StockEntities DbContext { get; set; }
        public IHistoricalDataProvider HistoricalDataProvider { get; set; }
        public IPriceProvider PriceProvider { get; set; }

        public PriceProviderTest()
        {
            InitDbContext();
            HistoricalDataProvider = new HistoricalDataProvider(DbContext);
            PriceProvider = new PriceProvider(HistoricalDataProvider);
        }

        void InitDbContext()
        {
            //https://msdn.microsoft.com/en-us/data/dn314429 - mocking dbcontext in ef 6
            var prices = new List<Price> 
            { 
                new Price 
                { 
                    Id=1,
                    CompanyId = 1,
                    DateTime = new DateTime(2015,1,1,10,0,0),
                    Volume = 1,
                    Close= 15
                }, 
                new Price 
                { 
                    Id=2,
                    CompanyId = 1,
                    DateTime = new DateTime(2015,1,1,10,5,1),
                    Volume = 1,
                    Close= 16
                }, 
                new Price 
                { 
                    Id=2,
                    CompanyId = 1,
                    DateTime = new DateTime(2015,1,1,10,7,0),
                    Volume = 1,
                    Close= 17
                }, 
                new Price 
                { 
                    Id=2,
                    CompanyId = 1,
                    DateTime = new DateTime(2015,1,1,10,10,0),
                    Volume = 1,
                    Close= 18
                }
            }.AsQueryable();

            var companies = new List<Company> 
            { 
                new Company 
                { 
                    Id = 1,
                    Name = "CIECH"
                }
            }.AsQueryable();

            var mockPrices = new Mock<DbSet<Price>>();
            mockPrices.As<IQueryable<Price>>().Setup(m => m.Provider).Returns(prices.Provider);
            mockPrices.As<IQueryable<Price>>().Setup(m => m.Expression).Returns(prices.Expression);
            mockPrices.As<IQueryable<Price>>().Setup(m => m.ElementType).Returns(prices.ElementType);
            mockPrices.As<IQueryable<Price>>().Setup(m => m.GetEnumerator()).Returns(prices.GetEnumerator());

            var mockCompanies = new Mock<DbSet<Company>>();
            mockCompanies.As<IQueryable<Company>>().Setup(m => m.Provider).Returns(companies.Provider);
            mockCompanies.As<IQueryable<Company>>().Setup(m => m.Expression).Returns(companies.Expression);
            mockCompanies.As<IQueryable<Company>>().Setup(m => m.ElementType).Returns(companies.ElementType);
            mockCompanies.As<IQueryable<Company>>().Setup(m => m.GetEnumerator()).Returns(companies.GetEnumerator());

            var mockContext = new Mock<StockEntities>();
            mockContext.Setup(m => m.Prices).Returns(mockPrices.Object);
            mockContext.Setup(m => m.Companies).Returns(mockCompanies.Object);

            DbContext = mockContext.Object;
        }

        [Fact]
        public void PriceProvider_GetClosestPrice1()
        {
            var result = PriceProvider.GetClosestPrice("CIECH", new DateTime(2015, 1, 1, 10, 9, 0));
            Assert.Equal(18, result);
        }

        [Fact]
        public void PriceProvider_GetClosestPrice2()
        {
            var result = PriceProvider.GetClosestPrice("CIECH", new DateTime(2015, 1, 1, 10, 8, 0));
            Assert.Equal(17, result);
        }

        [Fact]
        public void PriceProvider_GetClosestPrice3()
        {
            var result = PriceProvider.GetClosestPrice("CIECH", new DateTime(2015, 1, 1, 10, 4, 0));
            Assert.Equal(16, result);
        }

        [Fact]
        public void PriceProvider_GetClosestPrice4()
        {
            var result = PriceProvider.GetClosestPrice("CIECH", new DateTime(2015, 1, 1, 10, 6, 0));
            Assert.Equal(16, result);
        } 
    }
}

using NUnit.Framework;
using System.Collections.Generic;

namespace PriceBasket.UnitTests
{
    public class PricingCalculatorServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void subTotal_is_what_expected()
        {
            // Arrange
            ILogsService logService = new LogsService();
            IPricingCalculatorService service = new PricingCalculatorService(logService);

            IDictionary<string, IList<Item>> items = new Dictionary<string, IList<Item>>();
            Bread bread1 = new Bread() { Price = 0.30M };
            Bread bread2 = new Bread() { Price = 0.30M };
            items.Add("bread", new List<Item> { bread1, bread2}) ;
            decimal resultExpected = bread1.Price + bread2.Price; 

            // Act
            service.CalculatePrice(items);

            // Assert
            var result = decimal.Compare(resultExpected, service.getSubTotal());
            Assert.True(result == 0);
        }
    }
}
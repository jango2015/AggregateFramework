using System.Linq;
using App.Core.Products;
using App.DataAccess;
using App.DataAccess.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Tests.Product.Integration
{
    [TestClass]
    public class ChangePriceTests : EntityFrameworkIntegrationTest
    {
        [ClassInitialize]
        public static void Setup(TestContext ctx)
        {
            RefreshDatabase();
            var addProduct = new AddProduct("Name", "Description", new decimal(1.99));
            var sut = new ProductService(new EntityFrameworkRepository(DbContext),
                new EntityFrameworkProductLocator(DbContext));
            sut.Execute(addProduct);
            var productId = DbContext.Products.Single().Id;
            var changePrice = new ChangePrice(productId, new decimal(29.99));

            sut.Execute(changePrice);
        }

        [TestMethod]
        public void ItShouldChangeThePrice()
        {
            var product = DbContext.Products.Single();
            Assert.AreEqual(2999, product.Price);
        }
    }
}

using System.Linq;
using App.Core.Products;
using App.DataAccess;
using App.DataAccess.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Tests.Product.Integration
{
    [TestClass]
    public class DiscontinueProductTests : EntityFrameworkIntegrationTest
    {
        [ClassInitialize]
        public static void Setup(TestContext ctx)
        {
            RefreshDatabase();
            var addProduct = new AddProduct("Name", "Description", new decimal(1.99));
            var sut = new ProductService(new EntityFrameworkRepository(DbContext),
                new EntityFrameworkProductLocator(DbContext));
            sut.Execute(addProduct);
            var product = DbContext.Products.Single();
            Assert.IsFalse(product.Discontinued);
            var discontinueProduct = new DiscontinueProduct(product.Id);

            sut.Execute(discontinueProduct);
        }

        [TestMethod]
        public void ItShouldDiscontinueTheProduct()
        {
            var product = DbContext.Products.Single();
            Assert.IsTrue(product.Discontinued);
        }
    }
}

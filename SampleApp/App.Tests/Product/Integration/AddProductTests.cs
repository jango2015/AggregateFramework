using System.Linq;
using App.Core.Products;
using App.DataAccess;
using App.DataAccess.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Tests.Product.Integration
{
    [TestClass]
    public class AddProductTests : EntityFrameworkIntegrationTest
    {
        private static AddProduct _command;

        [ClassInitialize]
        public static void Setup(TestContext ctx)
        {
            RefreshDatabase();
            _command = new AddProduct("Name", "Description", new decimal(1.99));
            var sut = new ProductService(new EntityFrameworkRepository(DbContext), new EntityFrameworkProductLocator(DbContext));

            sut.Execute(_command);
        }

        [TestMethod]
        public void ItShouldAddTheProduct()
        {
            var product = DbContext.Products.Single();
            Assert.AreEqual(_command.Name, product.Name);
            Assert.AreEqual(_command.Description, product.Description);
            Assert.AreEqual(199, product.Price);
        }
    }
}

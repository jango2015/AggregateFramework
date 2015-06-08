using System.Linq;
using App.Core.Products;
using App.DataAccess;
using App.DataAccess.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Tests.Product.Integration
{
    [TestClass]
    public class FindProductByIdTests : EntityFrameworkIntegrationTest
    {
        private static AddProduct _addProduct;
        private static ProductDto _product;

        [ClassInitialize]
        public static void Setup(TestContext ctx)
        {
            RefreshDatabase();
            _addProduct = new AddProduct("Name", "Description", new decimal(1.99));
            var sut = new ProductService(new EntityFrameworkRepository(DbContext),
                new EntityFrameworkProductLocator(DbContext));
            sut.Execute(_addProduct);
            var productId = DbContext.Products.Single().Id;
            var findProductById = new FindProductById(productId);

            _product = sut.Execute(findProductById);
        }

        [TestMethod]
        public void ItShouldFindTheProduct()
        {
            Assert.AreEqual(_addProduct.Name, _product.Name);
            Assert.AreEqual(_addProduct.Description, _product.Description);
            Assert.AreEqual(_addProduct.Price, _product.Price);
        }
    }
}

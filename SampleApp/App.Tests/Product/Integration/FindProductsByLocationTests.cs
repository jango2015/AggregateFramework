using System.Collections.Generic;
using System.Linq;
using App.Core.Products;
using App.DataAccess;
using App.DataAccess.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Tests.Product.Integration
{
    [TestClass]
    public class FindProductsByLocationTests : EntityFrameworkIntegrationTest
    {
        private static IEnumerable<ProductDto> _products;

        [ClassInitialize]
        public static void Setup(TestContext ctx)
        {
            RefreshDatabase();
            //TODO: Add more products, some that shouldn't be returned by the query
            var addProduct = new AddProduct("Name", "Description", new decimal(1.99));
            var sut = new ProductService(new EntityFrameworkRepository(DbContext),
                new EntityFrameworkProductLocator(DbContext));
            sut.Execute(addProduct);
            var findProductsByLocation = new FindProductsByLocation(58.3, -134.4);

            _products = sut.Execute(findProductsByLocation);
        }

        [TestMethod]
        public void ItFindsTheProductsAvailable()
        {
            var id = DbContext.Products.Single().Id;
            var expectedResults = new[]
            {
                new ProductDto(id, "Name", "Description", new decimal(1.99))
            };
            var actualArray = _products.ToArray();
            CollectionAssert.AreEquivalent(expectedResults, actualArray);
        }
    }
}

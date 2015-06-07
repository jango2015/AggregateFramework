using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Tests.Product
{
    [TestClass]
    public class SetPriceTests
    {
        private static Core.Products.Product _sut;

        [ClassInitialize]
        public static void Setup(TestContext ctx)
        {
            _sut = new Core.Products.Product("Name", "Description", new decimal(1.99));
            _sut.SetPrice(new decimal(4.99));
        }

        [TestMethod]
        public void ItShouldConvertThePriceToPennies()
        {
            Assert.AreEqual(499, _sut.GetState().Price);
        }
    }
}

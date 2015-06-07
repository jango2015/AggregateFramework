using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Tests.Product
{
    [TestClass]
    public class DiscontinueTests
    {
        private static Core.Products.Product _sut;

        [ClassInitialize]
        public static void Setup(TestContext ctx)
        {
            _sut = new Core.Products.Product("Name", "Description", new decimal(1.99));
            _sut.Discontinue();
        }

        [TestMethod]
        public void ItShouldUpdateTheDiscontinuedFlag()
        {
            Assert.AreEqual(true, _sut.GetState().Discontinued);
        }
    }
}

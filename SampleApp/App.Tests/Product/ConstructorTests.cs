using App.Core.ReferenceData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Tests.Product
{
    [TestClass]
    public class ConstructorTests
    {
        private static Core.Products.Product _sut;

        [ClassInitialize]
        public static void Setup(TestContext ctx)
        {
            _sut = new Core.Products.Product("Name", "Description", new decimal(1.99));
        }

        [TestMethod]
        public void ItShouldConvertThePriceToPennies()
        {
            Assert.AreEqual(199, _sut.GetState().Price);
        }

        [TestMethod]
        public void ItShouldDefaultToDowntownJuneau()
        {
            Assert.AreEqual(Locations.DtJuneau, _sut.GetState().AvailablityArea);
        }

        [TestMethod]
        public void ItShouldBeAvailable()
        {
            Assert.AreEqual(false, _sut.GetState().Discontinued);
        }
    }
}

using System;
using AggregateFramework.Tests.TestDoubles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AggregateFramework.Tests.AbstractRepository
{
    [TestClass]
    public class SaveTests
    {
        private static TestRepository _sut;
        private static TestState _state;

        [ClassInitialize]
        public static void Setup(TestContext ctx)
        {
            _state = new TestState { Id = Guid.NewGuid() };
            var aggregate = new TestAggregate(_state);
            _sut = new TestRepository();
            
            _sut.Save(aggregate);
        }

        [TestMethod]
        public void ItShoudSaveTheState()
        {
            Assert.AreEqual(_state, _sut.LastSavedState);
        }
    }
}

using System;
using AggregateFramework.Tests.TestDoubles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AggregateFramework.Tests.AbstractRepository
{
    [TestClass]
    public class GetByIdTests
    {
        private static TestRepository _sut;
        private static TestAggregate _rehydratedAggregate;
        private static TestState _state;

        [ClassInitialize]
        public static void Setup(TestContext ctx)
        {
            _state = new TestState { Id = Guid.NewGuid() };
            _sut = new TestRepository(_state);

            _rehydratedAggregate = _sut.GetById<TestAggregate>(_state.Id);
        }

        [TestMethod]
        public void ItShouldExtractTheStateType()
        {
            Assert.AreEqual(typeof(TestState), _sut.LastTypeRetrieved);
        }

        [TestMethod]
        public void ItShouldRehydrateWithTheStateObject()
        {
            Assert.AreEqual(_state, _rehydratedAggregate.GetState());
        }
    }
}

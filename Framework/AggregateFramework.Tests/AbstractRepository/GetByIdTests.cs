using System;
using AggregateFramework.Tests.TestDoubles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AggregateFramework.Tests.AbstractRepository
{
    [TestClass]
    public class GetByIdTests
    {
        private static TestAggregate _rehydratedAggregate;
        private static TestState _state;

        [ClassInitialize]
        public static void Setup(TestContext ctx)
        {
            _state = new TestState { Id = Guid.NewGuid() };
            var sut = new TestRepository(_state);

            _rehydratedAggregate = sut.GetById<TestAggregate, TestState>(_state.Id);
        }

        [TestMethod]
        public void ItShouldRehydrateWithTheStateObject()
        {
            Assert.AreEqual(_state, _rehydratedAggregate.GetState());
        }
    }
}

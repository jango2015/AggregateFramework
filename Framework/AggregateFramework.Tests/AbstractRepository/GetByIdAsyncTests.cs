using System;
using System.Threading.Tasks;
using AggregateFramework.Tests.TestDoubles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AggregateFramework.Tests.AbstractRepository
{
    [TestClass]
    public class GetByIdAsyncTests
    {
        private static TestRepository _sut;
        private static TestState _state;

        [ClassInitialize]
        public static void Setup(TestContext ctx)
        {
            _state = new TestState { Id = Guid.NewGuid() };
            _sut = new TestRepository(_state);
        }

        [TestMethod]
        public async Task ItShouldExtractTheStateType()
        {
            await _sut.GetByIdAsync<TestAggregate>(_state.Id);  
          
            Assert.AreEqual(typeof(TestState), _sut.LastTypeRetrieved);
        }

        [TestMethod]
        public async Task ItShouldRehydrateWithTheStateObject()
        {
            var rehydratedAggregate = await _sut.GetByIdAsync<TestAggregate>(_state.Id);

            Assert.AreEqual(_state, rehydratedAggregate.GetState());
        }
    }
}

using System;
using AggregateFramework.Tests.TestDoubles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AggregateFramework.Tests.AbstractAggregate
{
    [TestClass]
    public class ConstructorTests
    {
        [TestMethod]
        public void ItShouldCreateADefaultStateObject()
        {
            var sut = new TestAggregate();
            
            Assert.IsNotNull(sut.GetState());
            Assert.AreEqual(typeof(TestState), sut.GetState().GetType());
        }

        [TestMethod]
        public void ItShouldSetTheState()
        {
            var state = new TestState {Id = Guid.NewGuid()};

            var sut = new TestAggregate(state);

            Assert.AreEqual(state, sut.GetState());
        }
    }
}

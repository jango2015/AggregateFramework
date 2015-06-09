using System;
using AggregateFramework.DataAccess;
using AggregateFramework.Tests.TestDoubles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AggregateFramework.Tests.ServiceBase
{
    [TestClass]
    public class ExecuteTests
    {
        private static Mock<IRepository> _repo;
        private static TestAggregate _aggregate;

        [ClassInitialize]
        public static void Setup(TestContext ctx)
        {
            _aggregate = new TestAggregate();
            _repo = new Mock<IRepository>();
            _repo.Setup(r => r.GetById<TestAggregate, TestState>(It.IsAny<Guid>())).Returns(_aggregate);
            var sut = new TestService(_repo.Object);

            sut.ExecuteAction(Guid.Empty, a => a.PerformAction());           
        }

        [TestMethod]
        public void ItShouldPerformTheAction()
        {
            Assert.IsTrue(_aggregate.ActionWasPerformed);
        }

        [TestMethod]
        public void ItShouldSaveTheAggregate()
        {
            _repo.Verify(r => r.Save(It.Is<TestAggregate>(a => a == _aggregate)), Times.Once);
        }

        [TestMethod]
        public void ItShouldCommitTheChanges()
        {
            _repo.Verify(r => r.Commit(), Times.Once);
        }
    }
}

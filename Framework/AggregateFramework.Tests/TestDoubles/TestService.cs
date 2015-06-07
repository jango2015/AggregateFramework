using System;
using AggregateFramework.DataAccess;

namespace AggregateFramework.Tests.TestDoubles
{
    internal class TestService : ServiceBase<TestAggregate, TestState>
    {
        public TestService(IRepository repo) : base(repo)
        {
        }

        public void ExecuteAction(Guid id, Action<TestAggregate> action)
        {
            Execute(id, action);
        }

        public void CallSaveAndCommit(TestAggregate aggregate)
        {
            SaveAndCommit(aggregate);
        }
    }
}

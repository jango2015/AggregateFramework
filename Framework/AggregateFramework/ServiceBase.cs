using System;
using AggregateFramework.DataAccess;

namespace AggregateFramework
{
    public abstract class ServiceBase<TAgg, TState>
        where TAgg : IAggregate<TState>
        where TState : class
    {
        protected readonly IRepository Repo;

        protected ServiceBase(IRepository repo)
        {
            Repo = repo;
        }

        protected void Execute(Guid id, Action<TAgg> action)
        {
            var aggregate = Repo.GetById<TAgg>(id);
            action(aggregate);
            SaveAndCommit(aggregate);
        }

        protected void SaveAndCommit(TAgg aggregate)
        {
            Repo.Save(aggregate);
            Repo.Commit();
        }
    }
}

using System;
using AggregateFramework.DataAccess;
using System.Threading.Tasks;

namespace AggregateFramework
{
    /// <summary>
    /// Provides a base class with some default behavior for common uses.
    /// </summary>
    /// <typeparam name="TAgg">The aggregate type. Must implement IAggregate.</typeparam>
    /// <typeparam name="TState">The state type that the aggregate contains. This is the type that is stored in the actual data store.</typeparam>
    public abstract class ServiceBase<TAgg, TState>
        where TAgg : IAggregate<TState>
        where TState : class
    {
        protected readonly IRepository Repo;

        protected ServiceBase(IRepository repo)
        {
            Repo = repo;
        }

        /// <summary>
        /// Fetch the aggregate with the given id, perform the action on it, then save and commit to the repository.
        /// </summary>
        /// <param name="id">Id of aggregate to perform the action on.</param>
        /// <param name="action">The action to perform on the aggregate before saving.</param>
        protected void Execute(Guid id, Action<TAgg> action)
        {
            var aggregate = Repo.GetById<TAgg, TState>(id);
            action(aggregate);
            SaveAndCommit(aggregate);
        }

        /// <summary>
        /// Fetch the aggregate with the given id, perform the action on it, then save and commit to the repository asynchronously.
        /// </summary>
        /// <param name="id">Id of aggregate to perform the action on.</param>
        /// <param name="action">The action to perform on the aggregate before saving.</param>
        protected async Task ExecuteAsync(Guid id, Action<TAgg> action)
        {
            var aggregate = await Repo.GetByIdAsync<TAgg, TState>(id);
            action(aggregate);
            await SaveAndCommitAsync(aggregate);
        }

        /// <summary>
        /// Saves an aggregate to the repository and commits.
        /// </summary>
        /// <param name="aggregate">The aggregate to save.</param>
        protected void SaveAndCommit(TAgg aggregate)
        {
            Repo.Save(aggregate);
            Repo.Commit();
        }

        /// <summary>
        /// Saves an aggregate to the repository and commits asynchronously.
        /// </summary>
        /// <param name="aggregate">The aggregate to save.</param>
        protected async Task SaveAndCommitAsync(TAgg aggregate)
        {
            Repo.Save(aggregate);
            await Repo.CommitAsync();
        }
    }
}

using System;
using System.Threading.Tasks;

namespace AggregateFramework.DataAccess
{
    /// <summary>
    /// Abstracts the storage/retrieval details from the state extraction/aggregrate rehydration
    /// </summary>
    public abstract class AbstractRepository : IRepository
    {
        /// <summary>
        /// Gets a rehydrated aggregate
        /// </summary>
        /// <typeparam name="T">Type of the aggregate</typeparam>
        /// <param name="id">Id of the aggregate</param>
        /// <returns>A rehydrated aggregate</returns>        
        public T GetById<T>(object id) where T : IAggregate
        {
            var stateType = new StateTypeExtractor<T>().ExtractStateType();
            var state = GetById(stateType, id);
            return RehydrateAggregate<T>(state);
        }

        /// <summary>
        /// Gets a rehydrated aggregate asynchronously
        /// </summary>
        /// <typeparam name="T">Type of the aggregate</typeparam>
        /// <param name="id">Id of the aggregate</param>
        /// <returns>A rehydrated aggregate</returns> 
        public async Task<T> GetByIdAsync<T>(object id) where T : IAggregate
        {
            var stateType = new StateTypeExtractor<T>().ExtractStateType();
            var state = await GetByIdAsync(stateType, id);
            return RehydrateAggregate<T>(state);
        }

        /// <summary>
        /// Stores the state of an aggregate
        /// </summary>
        /// <typeparam name="T">Type of the state to store</typeparam>
        /// <param name="aggregate">Aggregate containing the state to store</param>
        public void Save<T>(IAggregate<T> aggregate) where T : class
        {
            Save(aggregate.GetState());
        }

        /// <summary>
        /// Commits all changes to the repository made since the last commit
        /// </summary>
        public abstract void Commit();

        /// <summary>
        /// Asynchronously commits all changes to the repository made since the last commit
        /// </summary>
        /// <returns></returns>
        public abstract Task CommitAsync();

        /// <summary>
        /// Fetches the object from the repository
        /// </summary>
        /// <param name="type">Type of the object to get</param>
        /// <param name="id">Id of the object to get</param>
        /// <returns>An instance of the requested type</returns>
        protected abstract object GetById(Type type, object id);

        /// <summary>
        /// Fetches the object from the repository asynchronously
        /// </summary>
        /// <param name="type">Type of the object to get</param>
        /// <param name="id">Id of the object to get</param>
        /// <returns>An instance of the requested type</returns>
        protected abstract Task<object> GetByIdAsync(Type type, object id);

        /// <summary>
        /// Stores an object in the repository
        /// </summary>
        /// <typeparam name="T">The type of the object to be stored</typeparam>
        /// <param name="state">The object to store</param>
        protected abstract void Save<T>(T state) where T : class;

        /// <summary>
        /// Creates a concrete aggregate instance with the given state
        /// </summary>
        /// <typeparam name="T">Concrete type of the aggregate</typeparam>
        /// <param name="state">State of the aggregate</param>
        /// <returns>Rehydrated aggregate</returns>
        private static T RehydrateAggregate<T>(object state) where T : IAggregate
        {
            var aggregate = (T)Activator.CreateInstance(typeof(T), state);
            return aggregate;
        }
    }
}

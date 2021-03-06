﻿using System;
using System.Threading.Tasks;

namespace AggregateFramework.DataAccess
{
    /// <summary>
    /// Abstracts the storage/retrieval details from the state extraction/aggregrate rehydration.
    /// </summary>
    public abstract class AbstractRepository : IRepository
    {
        /// <summary>
        /// Gets a rehydrated aggregate.
        /// </summary>
        /// <typeparam name="TState">Type of the state class contained in TAgg.</typeparam>
        /// <typeparam name="TAgg">Type of the aggregate.</typeparam>
        /// <param name="id">Id of the aggregate.</param>
        /// <returns>A rehydrated aggregate of type TAgg containing its state of type TState.</returns>
        public TAgg GetById<TAgg, TState>(object id) 
            where TAgg : IAggregate
            where TState : class
        {            
            var state = GetById<TState>(id);
            return RehydrateAggregate<TAgg>(state);
        }

        /// <summary>
        /// Gets a rehydrated aggregate asynchronously.
        /// </summary>
        /// <typeparam name="TState">Type of the state class contained in TAgg.</typeparam>
        /// <typeparam name="TAgg">Type of the aggregate.</typeparam>
        /// <param name="id">Id of the aggregate.</param>
        /// <returns>A rehydrated aggregate of type TAgg containing its state of type TState.</returns>
        public async Task<TAgg> GetByIdAsync<TAgg, TState>(object id)
            where TAgg : IAggregate
            where TState : class
        {
            var state = await GetByIdAsync<TState>(id);
            return RehydrateAggregate<TAgg>(state);
        }

        /// <summary>
        /// Persists the state of an aggregate.
        /// </summary>
        /// <typeparam name="TState">Type of the state to persist.</typeparam>
        /// <param name="aggregate">Aggregate containing the state to persist.</param>
        public void Save<TState>(IAggregate<TState> aggregate) where TState : class
        {
            Save(aggregate.GetState());
        }

        /// <summary>
        /// Commits all changes to the repository made since the last commit.
        /// </summary>
        public abstract void Commit();

        /// <summary>
        /// Asynchronously commits all changes to the repository made since the last commit.
        /// </summary>
        /// <returns></returns>
        public abstract Task CommitAsync();

        /// <summary>
        /// Fetches the object from the repository.
        /// </summary>
        /// <typeparam name="T">Type of the object to be fetched.</typeparam>
        /// <param name="id">Id of the object to fetch.</param>
        /// <returns>An instance of the requested type.</returns>
        protected abstract T GetById<T>(object id) where T : class;

        /// <summary>
        /// Fetches the object from the repository asynchronously.
        /// </summary>
        /// <typeparam name="T">Type of the object to be fetched.</typeparam>
        /// <param name="id">Id of the object to fetch.</param>
        /// <returns>An instance of the requested type.</returns>
        protected abstract Task<T> GetByIdAsync<T>(object id) where T : class;

        /// <summary>
        /// Persists an object.
        /// </summary>
        /// <typeparam name="T">The type of the object to be persisted.</typeparam>
        /// <param name="state">The object to persist.</param>
        protected abstract void Save<T>(T state) where T : class;

        /// <summary>
        /// Creates a concrete aggregate instance with the given state.
        /// </summary>
        /// <typeparam name="TAgg">Concrete type of the aggregate.</typeparam>
        /// <param name="state">State of the aggregate.</param>
        /// <returns>Rehydrated aggregate.</returns>
        private static TAgg RehydrateAggregate<TAgg>(object state) where TAgg : IAggregate
        {
            var aggregate = (TAgg)Activator.CreateInstance(typeof(TAgg), state);
            return aggregate;
        }
    }
}

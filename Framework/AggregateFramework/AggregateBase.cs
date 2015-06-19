using System;

namespace AggregateFramework
{
    /// <summary>
    /// Starting point for aggregate classes. Provides a holder for the state of the aggregate.
    /// </summary>
    /// <typeparam name="T">Type of the state contained in the aggregate.</typeparam>
    public abstract class AggregateBase<T> : IAggregate<T> where T : class
    {
        protected readonly T State;

        protected AggregateBase(T state)
        {
            State = state;
        }

        protected AggregateBase()
        {
            State = Activator.CreateInstance<T>();
        }

        public T GetState()
        {
            return State;
        }
    }
}

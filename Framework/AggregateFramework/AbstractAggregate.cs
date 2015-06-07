using System;

namespace AggregateFramework
{
    public abstract class AbstractAggregate<T> : IAggregate<T> where T : class
    {
        protected readonly T State;

        protected AbstractAggregate(T state)
        {
            State = state;
        }

        protected AbstractAggregate()
        {
            State = Activator.CreateInstance<T>();
        }

        public T GetState()
        {
            return State;
        }
    }
}

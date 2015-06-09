using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AggregateFramework.Tests.TestDoubles
{
    internal sealed class TestRepository : DataAccess.AbstractRepository
    {
        public object LastSavedState { get; private set; }

        private readonly IDictionary<object, object> _repo = new Dictionary<object, object>();
        private readonly IDictionary<object, object> _trans = new Dictionary<object, object>();

        public TestRepository(params object[] startingState)
        {
            foreach (var entry in startingState)
            {
                Save(entry);
            }
            Commit();
        }

        public override void Commit()
        {
            PersistAndClearTransaction();
        }    

        public override Task CommitAsync()
        {
            PersistAndClearTransaction();
            return null;
        }

        protected override TState GetById<TState>(object id)
        {
            return (TState)_repo[id];
        }

        protected override Task<TState> GetByIdAsync<TState>(object id)
        {
            return Task.FromResult((TState)_repo[id]);
        }

        protected override void Save<T>(T state)
        {
            LastSavedState = state;
            object id = ((dynamic) state).Id;
            _trans[id] = state;
        }

        private void PersistAndClearTransaction()
        {
            foreach (var entry in _trans)
            {
                _repo[entry.Key] = entry.Value;
            }
            _trans.Clear();
        }
    }
}

using System.Threading.Tasks;

namespace AggregateFramework.DataAccess
{
    public interface IRepository : IUnitOfWork
    {
        TAgg GetById<TAgg, TState>(object id) where TAgg : IAggregate where TState : class;
        Task<TAgg> GetByIdAsync<TAgg, TState>(object id) where TAgg : IAggregate where TState : class; 
        void Save<TState>(IAggregate<TState> aggregate) where TState : class;        
    }
}

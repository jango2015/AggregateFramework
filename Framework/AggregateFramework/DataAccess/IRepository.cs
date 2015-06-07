using System.Threading.Tasks;

namespace AggregateFramework.DataAccess
{
    public interface IRepository : IUnitOfWork
    {
        T GetById<T>(object id) where T : IAggregate;
        Task<T> GetByIdAsync<T>(object id) where T : IAggregate; 
        void Save<T>(IAggregate<T> aggregate) where T : class;        
    }
}

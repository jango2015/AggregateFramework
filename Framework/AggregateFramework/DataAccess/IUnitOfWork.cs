using System.Threading.Tasks;

namespace AggregateFramework.DataAccess
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
    }
}

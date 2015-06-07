using System;
using System.Data.Entity;
using System.Threading.Tasks;
using AggregateFramework;
using AggregateFramework.DataAccess;

namespace App.DataAccess
{
    /// <summary>
    /// Provides an Entity Framework implementation of the application repository
    /// </summary>
    public class EntityFrameworkRepository : AbstractRepository
    {
        private readonly DbContext _context;

        public EntityFrameworkRepository(DbContext context)
        {
            _context = context;
        }

        public override void Commit()
        {
            _context.SaveChanges();
        }

        public override async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected override object GetById(Type type, object id)
        {                        
            return GetSetFor(type).Find(id);
        }

        protected override async Task<object> GetByIdAsync(Type type, object id)
        {            
            return await GetSetFor(type).FindAsync(id);
        }

        protected override void Save<T>(T state)
        {            
            var entry = _context.Entry(state);
            // If it's detached we'll add it, otherwise Entity's change tracking will pick it up so we do nothing
            if (entry.State == EntityState.Detached)
            {
                GetSetFor<T>().Add(state);
            }
        }

        private DbSet GetSetFor<T>() where T : class
        {
            var set = _context.Set<T>();
            ThrowIfSetIsNull(set, typeof(T));
            return set;
        }

        private DbSet GetSetFor(Type t)
        {
            var set = _context.Set(t);
            ThrowIfSetIsNull(set, t);
            return set;
        }

        private void ThrowIfSetIsNull(DbSet set, Type t)
        {
            if (set == null)
            {
                throw new TypeArgumentException(string.Format("DbContext {0} does not contain a set for type {1}.", _context,
                    t.FullName));
            }
        }
    }
}

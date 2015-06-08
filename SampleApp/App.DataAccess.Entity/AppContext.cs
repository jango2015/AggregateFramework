using System.Data.Entity;
using App.Core.Products;

namespace App.DataAccess
{
    public class AppContext : DbContext
    {
        public AppContext() : base("DefaultConnection")
        {
        }

        public AppContext(string connectionStringName) : base(connectionStringName)
        {
        }

        public DbSet<ProductState> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductState>()
                .ToTable("Products");
        } 
    }
}

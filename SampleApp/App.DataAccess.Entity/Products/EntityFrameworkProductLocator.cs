using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Linq;
using App.Core.Products;

namespace App.DataAccess.Products
{
    public class EntityFrameworkProductLocator : IProductLocator
    {
        private readonly DbSet<ProductState> _products;

        public EntityFrameworkProductLocator(DbContext context)
        {
            _products = context.Set<ProductState>();
            if (_products == null)
            {
                throw new InvalidOperationException(string.Format("DbContext {0} does not contain a set for ProductState.", context));
            }
        }

        public IEnumerable<ProductDto> FindProductsByLocation(double longitude, double latitude)
        {
            var location = DbGeography.FromText(string.Format("POINT({0} {1})", longitude, latitude), 4326);
            return _products.Where(p => !p.Discontinued && p.AvailablityArea.Intersects(location))
                .ToList()
                .Select(p => p.ToDto());
        }


        public ProductDto FindById(Guid id)
        {
            return _products.Single(p => p.Id == id)
                .ToDto();
        }
    }
}

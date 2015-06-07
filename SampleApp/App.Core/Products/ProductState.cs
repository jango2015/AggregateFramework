using System;
using System.Data.Entity.Spatial;

namespace App.Core.Products
{
    public class ProductState
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public DbGeography AvailablityArea { get; set; }
        public bool Discontinued { get; set; }
    }
}

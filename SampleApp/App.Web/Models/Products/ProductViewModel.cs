using System;

namespace App.Web.Models.Products
{
    public class ProductViewModel
    {
        public ProductViewModel(Guid id, string name, string description, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = string.Format("{0:C}", price);
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Price { get; private set; }
    }
}
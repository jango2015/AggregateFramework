using System;
using AggregateFramework;
using App.Core.ReferenceData;

namespace App.Core.Products
{
    public class Product : AggregateBase<ProductState>
    {
        public Product(string name, string description, decimal price)
        {
            State.Id = Guid.NewGuid();
            State.Name = name;
            State.Description = description;
            State.Price = DollarsAndCentsToPennies(price);
            State.AvailablityArea = Locations.DtJuneau;
            State.Discontinued = false;
        }

        public Product(ProductState state) : base(state)
        {
        }

        /// <summary>
        /// Updates the price of the product
        /// </summary>
        /// <param name="newPrice">The new price of the product</param>
        public void SetPrice(decimal newPrice)
        {
            State.Price = DollarsAndCentsToPennies(newPrice);
        }

        /// <summary>
        /// Discontinues the product
        /// </summary>
        public void Discontinue()
        {
            State.Discontinued = true;
        }

        private static int DollarsAndCentsToPennies(decimal dollarsAndCents)
        {
            return (int) (dollarsAndCents*100);
        }
    }
}

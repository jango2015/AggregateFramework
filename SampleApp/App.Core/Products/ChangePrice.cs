using System;

namespace App.Core.Products
{
    public class ChangePrice
    {
        public ChangePrice(Guid productId, decimal newPrice)
        {
            ProductId = productId;
            NewPrice = newPrice;
        }

        public Guid ProductId { get; private set; }
        public decimal NewPrice { get; private set; }
    }
}

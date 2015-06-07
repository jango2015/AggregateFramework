using System;

namespace App.Core.Products
{
    public class DiscontinueProduct
    {
        public DiscontinueProduct(Guid productId)
        {
            ProductId = productId;
        }

        public Guid ProductId { get; private set; }
    }
}

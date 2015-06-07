using System;

namespace App.Core.Products
{
    public class FindProductById
    {
        public FindProductById(Guid productId)
        {
            ProductId = productId;
        }

        public Guid ProductId { get; private set; }
    }
}

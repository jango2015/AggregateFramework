using System;
using System.Collections.Generic;

namespace App.Core.Products
{
    public interface IProductLocator
    {
        IEnumerable<ProductDto> FindProductsByLocation(double longitude, double latitude);
        ProductDto FindById(Guid id);
    }
}

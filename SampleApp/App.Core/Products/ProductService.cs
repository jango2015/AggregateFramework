using System.Collections.Generic;
using AggregateFramework;
using AggregateFramework.DataAccess;

namespace App.Core.Products
{
    public class ProductService : ServiceBase<Product, ProductState>
    {
        private readonly IProductLocator _locator;

        public ProductService(IRepository repo, IProductLocator locator) : base(repo)
        {
            _locator = locator;
        }

        public void Execute(AddProduct command)
        {
            var product = new Product(command.Name, command.Description, command.Price);
            SaveAndCommit(product);
        }

        public void Execute(ChangePrice command)
        {
            Execute(command.ProductId, p => p.SetPrice(command.NewPrice));
        }

        public void Execute(DiscontinueProduct command)
        {
            Execute(command.ProductId, p => p.Discontinue());
        }

        public IEnumerable<ProductDto> Execute(FindProductsByLocation command)
        {
            return _locator.FindProductsByLocation(command.Longitude, command.Latitude);
        }

        public ProductDto Execute(FindProductById command)
        {
            return _locator.FindById(command.ProductId);
        }
    }
}

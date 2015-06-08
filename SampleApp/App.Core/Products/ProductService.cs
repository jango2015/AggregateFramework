using System.Collections.Generic;
using AggregateFramework;
using AggregateFramework.DataAccess;
using System.Threading.Tasks;

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
            var product = CreateProduct(command);
            SaveAndCommit(product);
        }

        public async Task ExecuteAsync(AddProduct command)
        {
            var product = CreateProduct(command);
            await SaveAndCommitAsync(product);
        }        

        public void Execute(ChangePrice command)
        {
            Execute(command.ProductId, p => p.SetPrice(command.NewPrice));
        }

        public async Task ExecuteAsync(ChangePrice command)
        {
            await ExecuteAsync(command.ProductId, p => p.SetPrice(command.NewPrice));
        }

        public void Execute(DiscontinueProduct command)
        {
            Execute(command.ProductId, p => p.Discontinue());
        }

        public async Task ExecuteAsync(DiscontinueProduct command)
        {
            await ExecuteAsync(command.ProductId, p => p.Discontinue());
        }

        public IEnumerable<ProductDto> Execute(FindProductsByLocation command)
        {
            return _locator.FindProductsByLocation(command.Longitude, command.Latitude);
        }

        public ProductDto Execute(FindProductById command)
        {
            return _locator.FindById(command.ProductId);
        }

        private static Product CreateProduct(AddProduct command)
        {
            return new Product(command.Name, command.Description, command.Price);
        }
    }
}

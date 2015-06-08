using System;
using System.Linq;
using System.Web.Mvc;
using App.Core.Products;
using App.Web.Models.Products;
using System.Threading.Tasks;

namespace App.Web.Controllers
{   
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        // GET: Products
        public ActionResult Index()
        {
            var command = new FindProductsByLocation(58.3, -134.4);
            var products = _productService.Execute(command);
            return View(products.Select(p => p.ToViewModel()));
        }

        public ActionResult Edit(Guid id)
        {
            var product = FindProductById(id);
            return View(product.ToEditViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditProductViewModel product)
        {
            var command = new ChangePrice(product.Id, product.Price);
            await _productService.ExecuteAsync(command);
            return RedirectToAction("Index");
        }

        public ActionResult Discontinue(Guid id)
        {
            var product = FindProductById(id);
            return View(product.ToViewModel());
        }        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Discontinue(Guid id, FormCollection formCollection)
        {
            var command = new DiscontinueProduct(id);
            await _productService.ExecuteAsync(command);
            return RedirectToAction("Index");
        }

        private ProductDto FindProductById(Guid id)
        {
            var command = new FindProductById(id);
            return _productService.Execute(command);
        }
    }
}
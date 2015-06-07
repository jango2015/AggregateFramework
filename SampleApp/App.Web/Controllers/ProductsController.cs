using System;
using System.Linq;
using System.Web.Mvc;
using App.Core.Products;
using App.Web.Models.Products;

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
            var command = new FindProductById(id);
            var product = _productService.Execute(command);
            return View(product.ToEditViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditProductViewModel product)
        {
            var command = new ChangePrice(product.Id, product.Price);
            _productService.Execute(command);
            return RedirectToAction("Index");
        }

        public ActionResult Discontinue(Guid id)
        {
            var command = new FindProductById(id);
            var product = _productService.Execute(command);
            return View(product.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Discontinue(Guid id, FormCollection formCollection)
        {
            var command = new DiscontinueProduct(id);
            _productService.Execute(command);
            return RedirectToAction("Index");
        }
    }
}
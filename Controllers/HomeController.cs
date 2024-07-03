using eCommerceMvc.Models;
using Microsoft.AspNetCore.Mvc;
using eCommerceMvc.Models.Shared.Exceptions;
using Product = eCommerceMvc.Models.Product;

namespace eCommerceMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductCatalog productCatalog;

        public HomeController(IProductCatalog productCatalog)
        {
            this.productCatalog = productCatalog;
        }

        public IActionResult Index()
        {
            var productsList = this.productCatalog.GetProducts();
            return View(productsList.Result);
        }

        public IActionResult ProductDetails()
        {
            var id = 1;
            var product = this.productCatalog.GetProductDetails(id);
            if (product == null)
                throw new ProductNotFoundException();
            return View(product.Result);
        }

        public IActionResult AddToCart(long productId)
        {
            var product = productCatalog.GetProductDetails(productId);

            if (product != null)
            {
                ShoppingCart cart;

                if (HttpContext.Session.GetObjectFromJson<ShoppingCart>("ShoppingCart") == null)
                {
                    cart = new ShoppingCart();
                }
                else
                {
                    cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("ShoppingCart");
                }

                var existingProduct = cart.Products.FirstOrDefault(x => x.ProductId == product.Result.ProductId);
                if (existingProduct != null)
                {
                    existingProduct.Quantity += 1;
                }
                else
                {
                    cart.AddProduct(product.Result.MapToShoppingCartProduct());
                }

                HttpContext.Session.SetObjectAsJson("ShoppingCart", cart);
            }

            return RedirectToAction("Index", "Cart");
        }
    }
}

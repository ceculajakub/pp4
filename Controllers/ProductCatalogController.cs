using eCommerceMvc.Models;
using eCommerceMvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceMvc.Controllers
{
    [Controller]
    public class ProductCatalogController : Controller
    {
        private readonly IProductStorage productCatalog;

        public ProductCatalogController(IProductStorage productCatalog)
        {
            this.productCatalog = productCatalog;
        }

        [Route("/api/products")]
        public Task<IEnumerable<Product>> GetProducts()
        {
            var products = this.productCatalog.GetProducts();
            return products;
        }
    }
}

using eCommerce.Models;
using eCommerceMvc.Models;

namespace eCommerceMvc.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductStorage productRepository;

        public ProductService(IProductStorage productRepository)
        {
            this.productRepository = productRepository;
        }

        public void ChangePrice(long productId, decimal productPrice)
        {
            
            var product = this.productRepository.GetProductDetails(productId);
            product.Result.Price = productPrice;
            this.productRepository.UpdateProductAsync(product.Result);
        }

        public long CreateProduct( string productName, string productDescription)
        {
            return this.productRepository.AddProductAsync(new Product(productName, productDescription)).Result;
        }

        public long AddProduct(Product product)
        {
            return this.productRepository.AddProductAsync(product).Result;
        }

        public Task<IEnumerable<Product>> GetProducts()
        {
            return this.productRepository.GetProducts();
        }

        public Task<Product> GetProductDetails(long productId)
        {
            return this.productRepository.GetProductDetails(productId);
        }
    }

    public interface IProductService
    {
        void ChangePrice(long productId, decimal productPrice);
        long CreateProduct(string productName, string productDescription);

        long AddProduct(Product product);
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductDetails(long productId);
    }
}

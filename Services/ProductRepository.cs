using eCommerceMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceMvc.Services
{
    public class ProductRepository : IProductStorage
    {
        private readonly EcommerceContext _context;

        public ProductRepository(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductDetails(long productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<long> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.ProductId;
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(long productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            };
        }
    }

    public interface IProductStorage
    {
        Task<Product> GetProductDetails(long productId);
        Task<IEnumerable<Product>> GetProducts();
        Task<long> AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(long productId);

    }
}

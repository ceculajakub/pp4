using eCommerceMvc.Services;

namespace eCommerceMvc.Models;

public class ProductCatalog : IProductCatalog
{
    private readonly IProductService productService;

    public ProductCatalog(IProductService productService)
    {
        this.productService = productService;
    }

    public void AddProduct(Product product)
    {
        this.productService.AddProduct(product);
    }

    public Task<IEnumerable<Product>> GetProducts()
    {
        return this.productService.GetProducts();
    }

    public long CreateProduct(Product product)
    {
        return this.productService.AddProduct(product);
    }

    public Task<Product> GetProductDetails(long productId)
    {
        return this.productService.GetProductDetails(productId);
    }
    public void ChangePrice(long productId, decimal productPrice)
    {
        this.productService.ChangePrice(productId, productPrice);
    }

}

public interface IProductCatalog
{
    Task<IEnumerable<Product>> GetProducts();
    void AddProduct(Product product);
    long CreateProduct(Product product);
    Task<Product> GetProductDetails(long productId);
    void ChangePrice(long productId, decimal productPrice);
}


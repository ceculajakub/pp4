namespace eCommerceMvc.Models;

public class Product
{
    public Product(string productName, string productDescription)
    {
        
        ProductName = productName;
        ProductDescription = productDescription;
    }

    public long ProductId { get; private set; }
    public string ProductName { get; private set; }
    public string? ProductDescription { get; private set; }
    public decimal Price { get; set; }
    public string ProviderName { get; set; }

    public ShoppingCartProduct MapToShoppingCartProduct()
    {
        return new ShoppingCartProduct()
        {
            ProductId = this.ProductId,
            ProductName = this.ProductName,
            ProductDescription = this.ProductDescription,
            UnitPrice = this.Price,
            Quantity = 1,
            ProviderName = this.ProviderName
        };
    }
}
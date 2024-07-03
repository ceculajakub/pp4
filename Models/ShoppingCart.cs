namespace eCommerceMvc.Models;

public class ShoppingCart
{
    public List<ShoppingCartProduct> Products { get; set; } = new List<ShoppingCartProduct>();
    public decimal CartPrice => Products.Sum(x => x.TotalPrice);

    public void AddProduct(ShoppingCartProduct product)
    {
        Products.Add(product);
    }

    public void RemoveProduct(ShoppingCartProduct product)
    {
        Products.RemoveAll(x => x.ProductId == product.ProductId);
    }

    public IEnumerable<ShoppingCartProduct> GetProducts()
    {
        return Products;
    }

    public void Clear()
    {
        Products.Clear();
    }
}

public class ShoppingCartProduct
{
    public long ProductId { get; set; }
    public string ProductName { get; set; }
    public string? ProductDescription { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice => UnitPrice * Quantity;
    public string ProviderName { get; set; }
    public int Quantity { get; set; }
}
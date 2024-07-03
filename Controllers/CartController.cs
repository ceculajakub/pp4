using eCommerceMvc.Models;
using eCommerceMvc.Models.PayU;
using eCommerceMvc.Services;
using Microsoft.AspNetCore.Mvc;

public class CartController : Controller
{
    private readonly IProductStorage productRepository;
    private readonly PayU paymentGateway;
    private readonly IPayUMapper payUMapper;

    public CartController(IProductStorage productRepository, IPayUMapper payUMapper)
    {
        this.productRepository = productRepository;
        this.payUMapper = payUMapper;
        this.paymentGateway = new PayU(
            new HttpClient(),
            PayUCredentials.Sandbox(
                "300746",
                "2ee86a66e5d97e3fadc400c9f19b065d"
            ));
    }

    public IActionResult Index()
    {
        ShoppingCart cart;

        if (HttpContext.Session.GetObjectFromJson<ShoppingCart>("ShoppingCart") == null)
        {
            cart = new ShoppingCart();
            HttpContext.Session.SetObjectAsJson("ShoppingCart", cart);
        }
        else
        {
            cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("ShoppingCart");
        }

        return View("CartProductsList", cart);
    }

    public IActionResult ClearCart()
    {
        HttpContext.Session.Remove("ShoppingCart");
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> RemoveFromCart(long productId)
    {
        var product = productRepository.GetProductDetails(productId);

        if (product.Result != null)
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("ShoppingCart");
            if (cart != null)
            {
                var existingProduct = cart.Products.FirstOrDefault(x => x.ProductId == product.Result.ProductId);
                if (existingProduct?.Quantity > 1)
                    existingProduct.Quantity -= 1;
                else
                {
                    cart.RemoveProduct(product.Result.MapToShoppingCartProduct());
                }

                HttpContext.Session.SetObjectAsJson("ShoppingCart", cart);
            }
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Buy()
    {
        try
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("ShoppingCart");
            if (cart != null)
            {
                var request = this.payUMapper.OrderCreate(cart);
                var response = await paymentGateway.HandleAsync(request);
                if (response.statusCode == "OK")
                {
                    cart.Products = new List<ShoppingCartProduct>();
                    HttpContext.Session.SetObjectAsJson("ShoppingCart", cart);
                    return Redirect(response.redirectUri);
                }

                throw new Exception();
            }

            throw new Exception();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    public IActionResult OrderSuccess()
    {
        return View();
    }
}
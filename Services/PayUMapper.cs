using eCommerceMvc.Models;
using eCommerceMvc.Models.PayU;
using Product = eCommerceMvc.Models.PayU.Product;

namespace eCommerceMvc.Services
{
    public class PayUMapper : IPayUMapper
    {
        public OrderCreateRequest OrderCreate(ShoppingCart cart)
        {
            var request = new OrderCreateRequest
            {
                notifyUrl = "https://your.eshop.com/notify",
                customerIp = "127.0.0.1",
                merchantPosId = "300746",
                description = string.Join(",", cart.Products.Select(x => x.ProductId.ToString())),
                currencyCode = "PLN",
                totalAmount = (int)(cart.CartPrice * 100),
                extOrderId = Guid.NewGuid().ToString(),
                buyer = new Buyer
                {
                    email = "kuba.doe@example.com",
                    firstName = "john",
                    lastName = "doe",
                    language = "pl"
                },
                products = cart.Products.ConvertAll(x => new Product
                {
                    name = x.ProductName,
                    unitPrice = (int)(x.UnitPrice * 100),
                    quantity = x.Quantity
                }),
                continueUrl = "http://localhost:5010/Cart/OrderSuccess"
            };

            return request;
        }
    }

    public interface IPayUMapper
    {
        OrderCreateRequest OrderCreate(ShoppingCart cart);
    }
}

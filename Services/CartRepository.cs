using eCommerceMvc.Models;
using JetBrains.Annotations;

namespace eCommerceMvc.Services
{
    public class CartRepository : ICartRepository
    {
        public ShoppingCart GetForCustomer(string customerId)
        {
            return null;
        }
    }
    public interface ICartRepository
    {
        [CanBeNull]
        ShoppingCart GetForCustomer(string customerId);
    }
}

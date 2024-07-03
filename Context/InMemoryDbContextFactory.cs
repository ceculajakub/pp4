using eCommerceMvc.Models;
using Microsoft.EntityFrameworkCore;

public class InMemoryDbContextFactory
{
    public static EcommerceContext Create()
    {
        var options = new DbContextOptionsBuilder<EcommerceContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new EcommerceContext(options);

        context.Products.AddRange(
            new Product("Test Product 1", "Test Description 1") { Price = 10.99M, ProviderName = "Test Product 1" },
            new Product("Test Product 2", "Test Description 2") { Price = 20.99M, ProviderName = "Test Product 2" }
        );

        context.SaveChanges();

        return context;
    }

    public static void Destroy(EcommerceContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}
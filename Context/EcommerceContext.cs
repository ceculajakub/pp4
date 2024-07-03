using Microsoft.EntityFrameworkCore;
using eCommerceMvc.Models;

public class EcommerceContext : DbContext
{
    public EcommerceContext(DbContextOptions<EcommerceContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasKey(p => p.ProductId);

        modelBuilder.Entity<Product>()
            .Property(p => p.ProductId)
            .ValueGeneratedOnAdd();

        base.OnModelCreating(modelBuilder);
    }
}

public static class DbInitializer
{
    public static void Initialize(EcommerceContext context)
    {
        context.Database.EnsureCreated();

        if (context.Products.Any())
        {
            return;
        }

        var products = new Product[]
        {
            new Product("Guru Inwestowania Ujawnia Wszystko: 10 Tajemnic, Których Nigdy Ci Nie Powiedział",
                "Uzyskaj ekskluzywny dostęp do sekretnych strategii inwestowania, których żaden inny guru nie chce Ci ujawnić! W tym e-booku [imię guru], legendarny inwestor o wieloletnim doświadczeniu, dzieli się 10 kluczowymi zasadami, które pomogą Ci osiągnąć oszałamiający sukces na giełdzie. Odkryj, jak przewidywać ruchy rynku, wybierać obiecujące akcje i unikać kosztownych błędów.")
            {
                Price = 99.99m,
                ProviderName = "Akademia Bogactwa"
            },
            new Product("Szybka Droga do Bogactwa: Inwestycje w NFT dla Dzieci",
                "Szukasz szybkiego i łatwego sposobu na zbicie fortuny dla swojego dziecka? W takim razie ten e-book jest dla Ciebie! Odkryj fascynujący świat NFT i poznaj sprawdzone strategie inwestowania, które pomogą Twojemu dziecku błyskawicznie pomnożyć swoje kieszonkowe. Naucz się, jak znajdować unikalne i wartościowe NFT, budować sieć kolekcjonerów i czerpać zyski z ich naiwności.")
            {
                Price = 29.99M,
                ProviderName = "Fabryka Sukcesu"
            },
            new Product("Inwestycje w Złote Myśli: Jak Zostać Milionerem Pisząc Motywacyjne Cytaty", "")
            {
                Price = 59.99M,
                ProviderName = "Filantropia"
            }
        };

        foreach (var p in products)
        {
            context.Products.Add(p);
        }

        context.SaveChanges();
    }
}
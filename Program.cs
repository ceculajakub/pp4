using eCommerceMvc.Models;
using eCommerceMvc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<EcommerceContext>(options =>
    options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=FFF;Trusted_Connection=True;"));

builder.Services.AddTransient<IProductCatalog, ProductCatalog>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IPayUMapper, PayUMapper>();
builder.Services.AddScoped<IProductStorage, ProductRepository>();
builder.Services.AddHttpClient<PayU>();


builder.Services.AddMemoryCache();
builder.Services.AddSession();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<EcommerceContext>();
    DbInitializer.Initialize(context);
}


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.UseSession();

app.Run();

using SushiStore.Interfaces;
using SushiStore.Models;
using SushiStore.Repository;
using Microsoft.EntityFrameworkCore;
using SushiStore.Models;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddTransient<IProduct, ProductRepository>();

IConfigurationRoot _confString = new ConfigurationBuilder().
    SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();

builder.Services.AddDbContext<ApplicationContext>(options =>
               options.UseSqlServer(_confString.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<ICategory, CategoryRepository>();
builder.Services.AddTransient<IOrder, OrderRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

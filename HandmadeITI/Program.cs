using HandmadeITI.Core.Models;
using HandmadeITI.Data;
using HandmadeITI.Repos;
using HandmadeITI.Respo;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HandmadeITI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddScoped<IOrdersRepo, OrdersRepo>();
            builder.Services.AddScoped<OrderItemRepo, OrderItemRepo>();
            builder.Services.AddControllersWithViews();

            builder.Services.AddTransient<Irepo<Product>, ProductRepo>(); //hammad
            builder.Services.AddScoped<Irepo<Cart>, CartsRepo>();
            builder.Services.AddScoped<Irepo<CartItem>, CartItemsRepo>();
            builder.Services.AddScoped<Irepo<Category>, CategoriesRepo>();//taha
            builder.Services.AddScoped<Irepo<Review>, ReviewsRepo>();//taha

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}

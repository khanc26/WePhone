
<<<<<<< HEAD
using Stripe;
using WePhone.Controllers;
=======
using Microsoft.AspNetCore.Authentication.Cookies;
>>>>>>> 46244835a8587cd96493a7643ed62423f394e15c

namespace WePhone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            // Add services to the container.
<<<<<<< HEAD
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation() ;

            builder.Services.AddRazorPages();
=======
            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
            {
                option.LoginPath = "/Access/Login";
                option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
            }
);
>>>>>>> 46244835a8587cd96493a7643ed62423f394e15c

            var connectionSring = builder.Configuration.GetConnectionString("MySqlConn");

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(connectionSring, ServerVersion.AutoDetect(connectionSring));
            });

            builder.Services.Configure<StripeSetting>(builder.Configuration.GetSection("Stripe"));

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Set the session timeout as needed
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
            app.UseRouting();
<<<<<<< HEAD
            app.UseCors();
=======

            app.UseAuthentication();

>>>>>>> 46244835a8587cd96493a7643ed62423f394e15c
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Access}/{action=Login}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}
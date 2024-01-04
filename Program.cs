using Microsoft.AspNetCore.Authentication.Cookies;
using WePhone.Services;

namespace WePhone
{
    public class Program
    {

        public static void Main(string[] args) 
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            var smtpconfig = builder.Configuration.GetSection("SMTPConfig");
            builder.Services.Configure<SMTP_Model>(smtpconfig);
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Access/Login";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            });

            var connectionString = builder.Configuration.GetConnectionString("MySqlConn");

            builder.Services.AddDbContext<LoginContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
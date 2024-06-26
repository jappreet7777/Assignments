using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProductDetails.Application.Interface;
using ProductDetails.Application;
using ProductDetails.Infrastructure.ProductDetails;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ProductDetailsService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add serices to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("ProductDetailsDB");
            services.AddDbContext<ProductDBContext>(options => options.UseSqlServer(connectionString),ServiceLifetime.Transient);
            services.AddControllersWithViews();
            services.AddScoped<IProductDetailService, ProductDetailService>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
           

        }
    }
}

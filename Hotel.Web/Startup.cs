using Hotel.Core.Domain.Service.Tenant;
using Hotel.Core.Domain.Repositories.Tenant;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Hotel.Core.Domain.Service;
using Hotel.Core.Domain.Repositories;

namespace Hotel.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Đăng ký Services và Dependency Injection
        public void ConfigureServices(IServiceCollection services)
        {
            // MVC
            services.AddControllersWithViews();

            // MySQL Connection
            var connectionString = Configuration
                .GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "Database connection string is not configured. Set " +
                    "'ConnectionStrings__DefaultConnection' in Hotel.Web/.env " +
                    "or in the hosting environment.");
            }

            services.AddDbContext<HotelDbContext>(options =>
            {
                options.UseMySql(
                    connectionString,
                    new MySqlServerVersion(new Version(8, 0, 0))
                );
            });
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.UseRepository();
            services.UseService();
            // Đăng ký Infrastructure
            /// services.AddInfrastructure(Configuration);
        }

        // Cấu hình HTTP Middleware Pipeline

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            var webRootPath = env.WebRootPath;
            if (string.IsNullOrWhiteSpace(webRootPath))
            {
                webRootPath = Path.Combine(env.ContentRootPath, "wwwroot");
            }

            var webRootExists = Directory.Exists(webRootPath);
            Directory.CreateDirectory(
                Path.Combine(webRootPath, "uploads", "organizations"));
            if (!webRootExists)
            {
                env.WebRootFileProvider = new PhysicalFileProvider(webRootPath);
            }

            app.UseStaticFiles();

            // 1. Routing
            app.UseRouting();

            // 2. Authentication
            app.UseAuthentication();

            // 3. Authorization
            app.UseAuthorization();

            // 4. Map endpoints
            app.UseEndpoints(endpoints =>
            {
                // Area route
                endpoints.MapControllerRoute(
                    name: "withArea",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                // Default route
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}

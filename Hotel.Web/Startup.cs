using Hotel.Core.Domain.Service.Tenant;
using Microsoft.EntityFrameworkCore;

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
                    ServerVersion.AutoDetect(connectionString)
                );
            });
            services.AddControllers();
            services.AddScoped<OrganizationService>();

            // Đăng ký Infrastructure
            // services.AddInfrastructure(Configuration);
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

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}

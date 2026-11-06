using Hotel.Core.Domain.Service.Tenant;

namespace Hotel.Core.Domain.Service
{
    public static class ServiceExtension
    {
        public static void UseService(this IServiceCollection services)
        {
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<IHotelService, HotelService>();
        }
    }
}

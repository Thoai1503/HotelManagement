using Hotel.Core.Domain.Service.Others;
using Hotel.Core.Domain.Service.Tenant;
using Hotel.Core.Domain.Service.Identities;
namespace Hotel.Core.Domain.Service
{
    public static class ServiceExtension
    {
        public static void UseService(this IServiceCollection services)
        {
            services.AddTransient<IOrganizationService, OrganizationService>();
            services.AddTransient<IHotelService, HotelService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}

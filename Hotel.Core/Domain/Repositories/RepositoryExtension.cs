using Hotel.Core.Domain.Repositories.Others;
using Hotel.Core.Domain.Repositories.Tenant;
using Hotel.Core.Domain.Repositories.Identities;

namespace Hotel.Core.Domain.Repositories
{
    public static class RepositoryExtension
    {
        public static void UseRepository(this IServiceCollection services)
        {
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IHotelRepository, HotelRepository>();
            services.AddTransient<IOrganizationRepository, OrganizationRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAdminUserRepository, AdminUserRepository>();
        }
    }
}

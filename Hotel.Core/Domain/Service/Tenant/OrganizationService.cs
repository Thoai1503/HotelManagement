using Hotel.Core.Domain.Infrastructure.Models;

namespace Hotel.Core.Domain.Service.Tenant
{
    public class OrganizationService : BaseService<OrganizationModal>
    {
        public OrganizationService(OrganizationModal repository, IHttpContextAccessor httpContextAccessor) : base(repository, httpContextAccessor)
        {
        }
    }
}

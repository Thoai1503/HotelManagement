using Hotel.Core.Domain.Infrastructure.Models;
using Hotel.Core.Domain.Repositories.Tenant;

namespace Hotel.Core.Domain.Service.Tenant
{
    public class OrganizationService : BaseService<IOrganizationRepository> ,IOrganizationService
    {
        public OrganizationService(
            IOrganizationRepository repository,
            IHttpContextAccessor httpContextAccessor)
            : base(repository, httpContextAccessor)
        {
        }

        public Task<IReadOnlyList<OrganizationModal>> GetAll()
        {
            return Repository.GetAll();
        }

        public Task<OrganizationModal?> GetById(Guid id)
        {
            return Repository.GetById(id);
        }

        public Task<OrganizationModal> Create(OrganizationModal organization)
        {
            return Repository.Insert(organization);
        }

        public Task<OrganizationModal> Update(OrganizationModal organization)
        {
            return Repository.Update(organization);
        }

        public Task<OrganizationModal> Delete(OrganizationModal organization)
        {
            return Repository.Delete(organization);
        }
    }
}

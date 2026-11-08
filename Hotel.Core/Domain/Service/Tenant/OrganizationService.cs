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

        public Task<IReadOnlyList<OrganizationModel>> GetAll()
        {
            return Repository.GetAll();
        }

        public Task<OrganizationModel?> GetById(Guid id)
        {
            return Repository.GetById(id);
        }

        public Task<OrganizationModel> Create(OrganizationModel organization)
        {
            return Repository.Insert(organization);
        }

        public Task<OrganizationModel> Update(OrganizationModel organization)
        {
            return Repository.Update(organization);
        }

        public Task<OrganizationModel> Delete(OrganizationModel organization)
        {
            return Repository.Delete(organization);
        }
    }
}

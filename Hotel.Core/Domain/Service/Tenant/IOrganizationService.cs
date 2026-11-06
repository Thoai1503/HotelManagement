using Hotel.Core.Domain.Infrastructure.Models;

namespace Hotel.Core.Domain.Service.Tenant
{
    public interface IOrganizationService
    {
        Task<IReadOnlyList<OrganizationModal>> GetAll();
        Task<OrganizationModal?> GetById(Guid id);
        Task<OrganizationModal> Create(OrganizationModal organization);
        Task<OrganizationModal> Update(OrganizationModal organization);
        Task<OrganizationModal> Delete(OrganizationModal organization);
    }
}

using Hotel.Core.Domain.Infrastructure.Models;

namespace Hotel.Core.Domain.Service.Tenant
{
    public interface IOrganizationService
    {
        Task<IReadOnlyList<OrganizationModel>> GetAll();
        Task<OrganizationModel?> GetById(Guid id);
        Task<OrganizationModel> Create(OrganizationModel organization);
        Task<OrganizationModel> Update(OrganizationModel organization);
        Task<OrganizationModel> Delete(OrganizationModel organization);
    }
}

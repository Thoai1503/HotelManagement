using Hotel.Core.Domain.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Core.Domain.Repositories.Tenant
{
    public class OrganizationRepository : BaseRepository<OrganizationModal>, IOrganizationRepository
    {
        public OrganizationRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}

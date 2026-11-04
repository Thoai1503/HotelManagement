using Hotel.Core.Domain.Infrastructure.Models;
using Hotel.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Core.Domain.Repositories.Tenant
{
    public class OrganizationRepository : BaseRepository<OrganizationModal, Organization>, IOrganizationRepository
    {
        public OrganizationRepository(HotelDbContext dbContext) : base(dbContext)
        {
        }

    }
}

using Hotel.Core.Domain.Infrastructure.Models;
using HotelEntity = Hotel.Core.Domain.Entities.Hotel;

namespace Hotel.Core.Domain.Repositories.Tenant
{
    public class HotelRepository : BaseRepository<HotelModal, HotelEntity>, IHotelRepository
    {
        public HotelRepository(HotelDbContext dbContext) : base(dbContext)
        {
        }
    }
}

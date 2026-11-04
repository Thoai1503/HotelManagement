using Hotel.Core.Domain.Infrastructure.Models;
using Hotel.Core.Domain.Repositories.Tenant;

namespace Hotel.Core.Domain.Service.Tenant
{
    public class HotelService : BaseService<IHotelRepository>
    {
        public HotelService(
            IHotelRepository repository,
            IHttpContextAccessor httpContextAccessor)
            : base(repository, httpContextAccessor)
        {
        }

        public Task<IReadOnlyList<HotelModal>> GetAll()
        {
            return Repository.GetAll();
        }

        public Task<HotelModal?> GetById(Guid id)
        {
            return Repository.GetById(id);
        }

        public Task<HotelModal> Create(HotelModal hotel)
        {
            return Repository.Insert(hotel);
        }

        public Task<HotelModal> Update(HotelModal hotel)
        {
            return Repository.Update(hotel);
        }

        public Task<HotelModal> Delete(HotelModal hotel)
        {
            return Repository.Delete(hotel);
        }
    }
}

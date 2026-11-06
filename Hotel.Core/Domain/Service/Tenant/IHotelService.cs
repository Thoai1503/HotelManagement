using Hotel.Core.Domain.Infrastructure.Models;

namespace Hotel.Core.Domain.Service.Tenant
{
    public interface IHotelService
    {
        Task<IReadOnlyList<HotelModal>> GetAll();
        Task<HotelModal?> GetById(Guid id);
        Task<HotelModal> Create(HotelModal hotel);
        Task<HotelModal> Update(HotelModal hotel);
        Task<HotelModal> Delete(HotelModal hotel);
    }
}

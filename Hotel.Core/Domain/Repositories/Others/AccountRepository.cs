namespace Hotel.Core.Domain.Repositories.Others
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        public AccountRepository(HotelDbContext dbContext) : base(dbContext)
        {
        }
    }
}

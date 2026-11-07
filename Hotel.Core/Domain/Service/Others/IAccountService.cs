namespace Hotel.Core.Domain.Service.Others
{
    public interface IAccountService
    {
        bool IsPasswordMatched(string inputPassword, string password, string salt);

    }

}

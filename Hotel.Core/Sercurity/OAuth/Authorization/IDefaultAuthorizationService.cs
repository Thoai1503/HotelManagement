

using Hotel.Core.Domain.Infrastructure.Models;

public interface IDefaultAuthorizationService
{
    //Task<bool> AuthorizeAsync(string userId, string resource, string action);

    Task<UserModel> GetCurrentUser();
}
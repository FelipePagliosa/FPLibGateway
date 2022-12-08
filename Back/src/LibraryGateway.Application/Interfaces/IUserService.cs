using LibraryGateway.Application.Requests.UserRequests;
using LibraryGateway.Domain.Models;

namespace LibraryGateway.Application.Interfaces;

public interface IUserService
{
    Task<List<User>> GetAll();
    Task<User> GetUserByUserNameAsync(string userName);
    Task<bool> CheckUserPassword(string passwordHash, string passwordTyped);
    Task Add(UserInsertRequest userInsertRequest);
    Task Update(UserUpdateRequest request);
    Task Delete(int userId);
    Task ChangePassword(UserChangePasswordRequest userChangePasswordRequest);
}

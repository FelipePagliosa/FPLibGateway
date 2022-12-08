using LibraryGateway.Domain.Models;

namespace LibraryGateway.Domain.Repository;

public interface IUserRepository : IBaseRepository<User>
{
    Task<List<User>> GetUsersAsync();
    Task<User> GetUserByIdAsync(int id);
    Task<User> GetUserByUserNameAsync(string userName);
}

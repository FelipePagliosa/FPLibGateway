using System.Linq;
using Microsoft.EntityFrameworkCore;
using LibraryGateway.Domain.Models;
using LibraryGateway.Domain.Repository;
using LibraryGateway.Infra.Context;

namespace LibraryGateway.Infra.Repository.Identity;

public class UserRepository : BaseRepository<User>, IUserRepository
{

    public UserRepository(LibraryGatewayContext context) : base(context) {}

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _context.User.FindAsync(id);
    }

    public async Task<User> GetUserByUserNameAsync(string userName)
    {
        return await _context.User.FirstOrDefaultAsync(x=>(x.UserName.ToUpper()).Contains(userName.ToUpper()));
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await _context.User.ToListAsync();
    }
}

using LibraryGateway.Domain.Models;

namespace LibraryGateway.Application.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(User user);

}

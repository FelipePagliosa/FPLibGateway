using LibraryGateway.Domain.Enums;

namespace LibraryGateway.Application.Requests.UserRequests;

public class UserUpdateRequest
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public Perfil Perfil { get; set; }
    public bool Ativo { get; set; }

}

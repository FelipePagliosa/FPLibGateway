using LibraryGateway.Domain.Enums;

namespace LibraryGateway.Application.Requests.UserRequests;

public class UserInsertRequest
{
    public string Nome { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public Perfil Perfil { get; set; }
    public string Senha { get; set; }
    public bool Ativo { get; set; }
}

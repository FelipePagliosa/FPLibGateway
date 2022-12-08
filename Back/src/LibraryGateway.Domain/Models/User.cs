using System.Runtime.Serialization;
using LibraryGateway.Domain.Enums;

namespace LibraryGateway.Domain.Models;

public class User : BaseModel
{
    public string Nome { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public Perfil Perfil { get; set; }

    [IgnoreDataMemberAttribute]
    public string Senha { get; set; }
    
    public bool Ativo { get; set; }

}

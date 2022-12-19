using LibraryGateway.Domain.Enums;

namespace LibraryGateway.Application.Requests.LivroRequests;

public class LivroInsertRequest
{
    public string Nome { get; set; }
    public string Autor { get; set; }
    public string QuantidadePaginas { get; set; }
}

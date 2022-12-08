using LibraryGateway.Domain.Repository;

namespace LibraryGateway.Domain.Repository;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    void Iniciar();
    bool Commitar();
    Task<bool> CommitarAsync();
}

using Microsoft.EntityFrameworkCore;
using LibraryGateway.Domain.Repository;
using LibraryGateway.Infra.Context;
using LibraryGateway.Infra.Repository.Identity;

namespace LibraryGateway.Infra.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly LibraryGatewayContext context;


    public UnitOfWork(LibraryGatewayContext context)
    {
        this.context = context;
        this.UserRepository = new UserRepository(context);

    }

    public IUserRepository UserRepository { get; }


    public void Iniciar()
    {
        foreach (var entry in this.context.ChangeTracker.Entries().ToList())
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.State = EntityState.Unchanged;
                    break;
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
            }
        }
    }

    public bool Commitar()
    {
        return (this.context.SaveChanges()) > 0;
    }

    public async Task<bool> CommitarAsync()
    {
        return (await this.context.SaveChangesAsync()) > 0;
    }
}


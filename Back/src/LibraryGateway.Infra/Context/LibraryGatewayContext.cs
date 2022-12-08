using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryGateway.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryGateway.Infra.Context;

public class LibraryGatewayContext : DbContext
{
    public LibraryGatewayContext(DbContextOptions<LibraryGatewayContext> options) : base(options) { }
    public DbSet<User> User { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}

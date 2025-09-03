using AuthService.Domain.Entities;
using AuthService.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure;

public class AuthServiceDbContext : DbContext
{
    public  AuthServiceDbContext(DbContextOptions<AuthServiceDbContext> options) : base(options)
    {}
    
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<UserRole>();
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthServiceDbContext).Assembly);
    }
}
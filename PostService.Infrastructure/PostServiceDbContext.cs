using Microsoft.EntityFrameworkCore;
using PostService.Domain.Entities;

namespace PostService.Infrastructure;

public class PostServiceDbContext : DbContext
{
    public PostServiceDbContext(DbContextOptions<PostServiceDbContext> options) : base(options)
    {
    }
    
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostServiceDbContext).Assembly);
    }
}
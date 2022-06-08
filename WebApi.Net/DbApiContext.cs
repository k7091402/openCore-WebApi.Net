using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
public class DbApiContext : DbContext
{
    public DbSet<Response> Responses { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Response>(r => r.HasNoKey());
    }

    public DbApiContext(DbContextOptions<DbApiContext> options) : base(options)
    {
    }
}

public class Response
{
    //[Key]
    public string? result { get; set; }
}
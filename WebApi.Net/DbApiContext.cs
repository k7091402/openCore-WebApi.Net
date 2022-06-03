using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
public class DbApiContext : DbContext
{
    public DbSet<Response> Responses { get; set; } = null!;

    public DbApiContext(DbContextOptions<DbApiContext> options) : base(options)
    {
    }
}

public class Response
{
    [Key]
    public string? result { get; set; }
}
using Microsoft.EntityFrameworkCore;

namespace Authorisation.Models;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options)
        : base(options)
    {
    }

    public DbSet<UserItem> Users { get; set; } = null!;
}
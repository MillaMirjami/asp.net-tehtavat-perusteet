using Microsoft.EntityFrameworkCore;
using Authorisation.Models;

namespace Authorisation.Data
{

    // UserContext is a DbContext class used with EntityFrameworkCore.
    // UserContext represents the connection to the db
    // Enalbes the CRUD -operations (Create, Read, Update, Delete)
    // This class configures which tables or entities the app uses
    // and how they're mapped within the database.
    public class UserContext : DbContext // DbContext offers methods (ex. Add(), Remove(), SaveChanges())
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        } // Constructor, defines how DbContext is initialized


        public DbSet<UserItem> Users { get; set; } = null!;  // = null! tells the translator that even tho
                                                             //  the table is not initialized it's not null 
                                                             // and will be initialized before usage
        // <UserItem> = <Entity>
        // DbSet represents the collection of db tables
    }

}
using Microsoft.EntityFrameworkCore;
using Tasks.Models;

namespace Tasks.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using Tasks.Models;

namespace Task.DAL.EF
{
    public class PostgreeContext : DbContext
    {
        public PostgreeContext(DbContextOptions<PostgreeContext> options)
           : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Tasks.Models.Task> Tasks => Set<Tasks.Models.Task>();
    }
}
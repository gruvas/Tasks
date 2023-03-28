using Microsoft.EntityFrameworkCore;

namespace Task.DAL.EF
{
    public class PostgreeContext : DbContext
    {
        public PostgreeContext(DbContextOptions<PostgreeContext> options)
           : base(options)
        {
        }


        //public DbSet<User> Users => Set<User>();
        //public DbSet<T.Task> Tasks => Set<T.Task>();
    }
}
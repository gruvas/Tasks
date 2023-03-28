﻿using Microsoft.EntityFrameworkCore;
using Tasks.Domain.Models.Users;
using T = Tasks.Domain.Models.Tasks;

namespace Task.DAL.EF
{
    public class PostgreeContext : DbContext
    {
        //public PostgreeContext(DbContextOptions<PostgreeContext> options)
        //   : base(options)
        //{
        //}


        public PostgreeContext(DbContextOptions<PostgreeContext> options) : base(options)
        {
        }
        public DbSet<User> Users => Set<User>();
        public DbSet<T.Task> Tasks => Set<T.Task>();

        //public DbSet<User> Users { get; set; }
        //public DbSet<T.Task> Tasks { get; set; }
    }
}
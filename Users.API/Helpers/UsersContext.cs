using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.API.Entities;

namespace Users.API.Helpers
{
    public class UsersContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UsersContext(DbContextOptions<UsersContext> options)
           : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Email = "omar.aceves@mymail.com",
                    Id = Guid.NewGuid(),
                    Password = "password",
                    UserName = "omaraceves",
                    WatchLaterId = Guid.NewGuid()

                },
                new User()
                {
                    Email = "karen.aceves@mymail.com",
                    Id = Guid.NewGuid(),
                    Password = "password",
                    UserName = "karenaceves",
                    WatchLaterId = Guid.NewGuid()

                },
                new User()
                {
                    Email = "milo.woof@mymail.com",
                    Id = Guid.NewGuid(),
                    Password = "password",
                    UserName = "milowoof",
                    WatchLaterId = Guid.NewGuid()
                }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}


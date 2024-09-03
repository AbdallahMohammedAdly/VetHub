using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VetHub02.Core.Entities;

namespace VetHub02.Repository.Data
{
    public class StoreContext : DbContext
    {
      
        public StoreContext(DbContextOptions<StoreContext> options):base(options)
        {
         
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
           

            modelBuilder.Entity<Join>().Property(J => J.Gender).HasColumnType("bit");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Join> Joins { get; set; }

        public DbSet<History> Historys { get; set; }

        public DbSet<ContactUs> ContactUs { get; set; }
    }
}

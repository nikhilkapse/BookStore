using Book.Models;
using Microsoft.EntityFrameworkCore;

namespace Book.DataAccess.Data
{
    public class ApplicationDbContext : DbContext // DbContext class is root class of entity framework, any class that you have here must implement DbContext class
    {
        // :base(options) syntax is used to pass whatever options configured here to pass DbContext class of entity framework
        // this is .net syntax to pass configured options to base class
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
                );
        }
    }
}

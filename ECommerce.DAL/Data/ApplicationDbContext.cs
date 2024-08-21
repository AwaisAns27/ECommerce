using ECommerce.Model;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Catogeries { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electonic", IsActive = true, MaxTime = "ERROR" },
                new Category { Id = 2, Name = "Fashion", IsActive = true, MaxTime = "ERROR" },
                new Category { Id = 3, Name = "Cosmetic", IsActive = true, MaxTime = "ERROR" },
                new Category { Id = 4, Name = "Grocery", IsActive = true, MaxTime = "ERROR" }
                );
        }
    }
}

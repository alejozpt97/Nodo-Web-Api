using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User") .HasKey(u => u.Id);

            modelBuilder.Entity<User>().Property(u => u.Id).IsRequired().HasColumnName("id");
            modelBuilder.Entity<User>().Property(u => u.Name).IsRequired().HasColumnName("name");
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired().HasColumnName("email");
            modelBuilder.Entity<User>().Property(u => u.Age).IsRequired().HasColumnName("age");
                
        }
       
    }
}
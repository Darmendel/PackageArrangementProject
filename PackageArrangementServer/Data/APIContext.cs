#nullable disable
using Microsoft.EntityFrameworkCore;
using PackageArrangementServer.Models;

namespace PackageArrangementServer.Data
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> options)
            : base(options)
        {
        }

        public APIContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the Name property as the primary
            // key of the Items table
            modelBuilder.Entity<User>().HasKey(e => e.Id);
            modelBuilder.Entity<Delivery>().HasKey(e => new { e.Id, e.UserId }).HasName("PK_Contact");
        }

        public DbSet<User> Users { get; set; }
        //public DbSet<UserList> UserList { get; set; }
    }
}

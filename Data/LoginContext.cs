using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WePhone.Data
{
    public class LoginContext : DbContext
    {
        public LoginContext(DbContextOptions<LoginContext> options) : base(options)
        {
        }

        public DbSet<ULogin> ULogins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ULogin>().ToTable("users");
        }
    }
}
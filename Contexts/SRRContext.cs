
using Microsoft.EntityFrameworkCore;
using srr.Models;
namespace srr.Contexts
{
    public class SRRContext : DbContext
    {
        public SRRContext(DbContextOptions<SRRContext> options)
            : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserModel>()
                .HasKey(u => u.UserId);

            builder.Entity<UserModel>()
                .Property(u => u.UserId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Entity<UserModel>()
                .Property(u => u.Name)
                .HasMaxLength(50);

            builder.Entity<UserModel>()
                .Property(u => u.Email)
                .HasMaxLength(50);

            builder.Entity<UserModel>()
                .Property(u => u.AddressNo)
                .HasMaxLength(50);

            builder.Entity<UserModel>()
                .Property(u => u.DeedNo)
                .HasMaxLength(8);
        }
    }
}
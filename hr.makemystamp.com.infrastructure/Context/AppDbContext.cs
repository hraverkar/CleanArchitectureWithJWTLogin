using hr.makemystamp.com.application.Interfaces;
using hr.makemystamp.com.core.Entities;
using Microsoft.EntityFrameworkCore;

namespace hr.makemystamp.com.infrastructure.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(y => y.PhoneNumber).IsUnique();
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Identity> Identities { get; set; }
        public DbSet<RolesUser> RolesUsers { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }


    }
}

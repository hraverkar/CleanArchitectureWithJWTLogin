using hr.makemystamp.com.core.Entities;
using Microsoft.EntityFrameworkCore;

namespace hr.makemystamp.com.application.Interfaces
{
    public interface IAppDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Identity> Identities { get; set; }
        public DbSet<RolesUser> RolesUsers { get; set; }
        Task<int> SaveChangesAsync();
    }
}

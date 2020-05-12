using Microsoft.EntityFrameworkCore;

namespace WorldOfPets.Models
{
    public class UsersContext : DbContext
    {
        public DbSet<InfoUsers> Info_Users { get; set; }
        public DbSet<InfoAuth> Info_Auth { get; set; }
        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

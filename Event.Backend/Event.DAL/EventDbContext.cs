using Event.DAL.EntityTypeConfigurations;
using Event.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Event.DAL
{
    public class EventDbContext : DbContext
    {
        public DbSet<EventEntity> Events { get; set; }
        public DbSet<AccountEntity> Account { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }

        public EventDbContext(DbContextOptions<EventDbContext> options) 
            : base(options) 
        {
            AppContext.SetSwitch(switchName: "Npgsql.EnableLegacyTimestampBehavior", isEnabled: true);

            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EventConfiguration());

            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}

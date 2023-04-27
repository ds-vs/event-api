using Event.DAL.EntityTypeConfigurations;
using Event.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Event.DAL
{
    public class EventDbContext : DbContext
    {
        public DbSet<EventEntity> Events { get; set; }

        public EventDbContext(DbContextOptions<EventDbContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EventConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}

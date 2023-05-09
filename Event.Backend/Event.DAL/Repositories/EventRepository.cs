using Event.Domain.Entities;
using Event.Domain.Enums;
using Event.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Event.DAL.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventDbContext _context;

        public EventRepository(EventDbContext context)
        {
            _context = context;
        }

        public async Task CreateAccountToEventAsync(Guid accountId, Guid eventId)
        {
            var eventEntity = await _context.Events.Include(e => e.Accounts).FirstAsync(e => e.EventId == eventId);
            var accountEntity = await _context.Account
                .Include(x => x.Role)
                .FirstAsync(e => e.AccountId == accountId);

            eventEntity.Accounts.Add(accountEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<EventEntity> CreateAsync(EventEntity entity)
        {
            await _context.Events.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Events.FirstOrDefaultAsync(e => e.EventId == id);

            _context.Events.Remove(entity!);

            await _context.SaveChangesAsync();
        }

        public IQueryable<EventEntity> Get()
        {
            return _context.Events.AsQueryable();
        }

        public async Task<EventEntity> UpdateAsync(EventEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateEventStatusAsync(IQueryable<EventEntity> events)
        {
            events.ExecuteUpdate(e => e.SetProperty(p => p.Status, StatusType.Completed));

            await _context.SaveChangesAsync();
        }
    }
}

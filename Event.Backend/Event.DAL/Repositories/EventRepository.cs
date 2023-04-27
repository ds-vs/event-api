using Event.DAL.Repositories.Interfaces;
using Event.Domain.Entities;

namespace Event.DAL.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventDbContext _context;

        public EventRepository(EventDbContext context)
        {
            _context = context;
        }

        public IQueryable<EventEntity> Get()
        {
            return _context.Events.AsQueryable();
        }
    }
}

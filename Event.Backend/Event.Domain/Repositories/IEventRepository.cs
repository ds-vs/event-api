using Event.Domain.Entities;

namespace Event.Domain.Repositories.Interfaces
{
    public interface IEventRepository
    {
        IQueryable<EventEntity> Get();
        Task<EventEntity> CreateAsync(EventEntity entity);
        Task<EventEntity> UpdateAsync(EventEntity entity);
        Task UpdateEventStatusAsync(IQueryable<EventEntity> events);
        Task DeleteAsync(Guid id);
        Task CreateAccountToEventAsync(Guid accountId, Guid eventId);
    }
}

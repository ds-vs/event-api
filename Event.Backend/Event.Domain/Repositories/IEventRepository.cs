using Event.Domain.Entities;

namespace Event.Domain.Repositories.Interfaces
{
    public interface IEventRepository
    {
        IQueryable<EventEntity> Get();
        Task<EventEntity> CreateAsync(EventEntity entity);
        Task CreateAccountToEventAsync(Guid accountId, Guid eventId);
        Task DeleteAccountToEventAsync(Guid accountId, Guid eventId);
        Task<IEnumerable<EventEntity>> GetAccountToEntityAsync();
        Task<EventEntity> UpdateAsync(EventEntity entity);
        Task UpdateEventStatusAsync(IQueryable<EventEntity> events);
        Task UpdateEventResponseAsync(EventEntity entity);
        Task DeleteAsync(Guid id);
    }
}

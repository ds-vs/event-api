using Event.Domain;
using Event.Domain.Dto.Event;

namespace Event.Service.Interfaces
{
    public interface IEventService
    {
        IResponse<IEnumerable<GetEventDto>> Get();
        IResponse<GetEventDto> Get(Guid id);
        Task<IResponse<IEnumerable<GetEventDto>>> Get(string login);
        Task<IResponse<GetEventDto>> CreateEventAsync(string login, CreateEventDto eventDto);
        Task<IResponse<GetEventDto>> UpdateEventAsync(string login, Guid id, CreateEventDto eventDto);
        Task UpdateEventStatusAsync();
        Task<IResponse<IEnumerable<GetEventDto>>> GetSubscriptions(string login);
        Task<IResponse<bool>> EventSubscribeAsync(string login, Guid eventId);
        Task<IResponse<bool>> EventUnsubscribeAsync(string login, Guid eventId);
        Task<IResponse<GetEventDto>> DeleteEventAsync(string login, Guid id);
    }
}

using Event.Domain;
using Event.Domain.Dto.Event;
using Event.Domain.Entities;

namespace Event.Service.Interfaces
{
    /// <summary> Сервис отвечающий за бизнес логику, связанную с организацией мероприятий. </summary>
    public interface IEventService
    {
        /// <summary> Получение всех <see cref="EventEntity"/>. </summary>
        /// <returns> <see cref="Response{T}"/> </returns>
        IResponse<IEnumerable<GetEventDto>> Get();
        IResponse<GetEventDto> Get(Guid id);
        Task<IResponse<IEnumerable<GetEventDto>>> Get(string login);
        Task<IResponse<GetEventDto>> CreateEventAsync(string login, CreateEventDto eventDto);
        Task<IResponse<GetEventDto>> UpdateEventAsync(string login, Guid id, CreateEventDto eventDto);
        Task UpdateEventStatusAsync();
        Task<IResponse<GetEventDto>> DeleteEventAsync(string login, Guid id);
    }
}

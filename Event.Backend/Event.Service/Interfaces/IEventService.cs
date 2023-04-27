using Event.Domain;
using Event.Domain.Entities;

namespace Event.Service.Interfaces
{
    /// <summary> Сервис отвечающий за бизнес логику, связанную с организацией мероприятий. </summary>
    public interface IEventService
    {
        /// <summary> Получение всех <see cref="EventEntity"/>. </summary>
        /// <returns> <see cref="Response{T}"/> </returns>
        IResponse<IEnumerable<EventEntity>> Get();
    }
}

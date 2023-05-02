using Event.Domain.Entities;

namespace Event.Domain.Repositories.Interfaces
{
    /// <summary> Интерфейс репозитория для <see cref="EventEntity"/>. </summary>
    public interface IEventRepository
    {
        /// <summary> Метод для получения всех <see cref="EventEntity"/>. </summary>
        /// <returns> <see cref="EventEntity"/> коллекция. </returns>
        IQueryable<EventEntity> Get();

        /// <summary> Асинхронный медот для создания мероприятия в БД. </summary>
        /// <param name="entity"> Сущность мероприятия <see cref="EventEntity"/>. </param>
        /// <returns> <see cref="EventEntity"/>. </returns>
        Task<EventEntity> CreateAsync(EventEntity entity);

        /// <summary> Асинхронный метод для изменения данных о мероприятиях в БД. </summary>
        /// <param name="entity"> Сущность мероприятия <see cref="EventEntity"/>. </param>
        /// <returns> <see cref="EventEntity"/>. </returns>
        Task<EventEntity> UpdateAsync(EventEntity entity);

        Task UpdateEventStatusAsync(IQueryable<EventEntity> events);

        Task DeleteAsync(Guid id);
    }
}

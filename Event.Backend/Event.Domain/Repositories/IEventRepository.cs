using Event.Domain.Entities;

namespace Event.DAL.Repositories.Interfaces
{
    /// <summary> Интерфейс репозитория для <see cref="EventEntity"/>. </summary>
    public interface IEventRepository
    {
        /// <summary> Получение всех <see cref="EventEntity"/>. </summary>
        /// <returns> <see cref="EventEntity"/> коллекция. </returns>
        IQueryable<EventEntity> Get();
    }
}

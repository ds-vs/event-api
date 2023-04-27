using Event.Domain.Enums;

namespace Event.Domain.Entities
{
    /// <summary> Сущность описывающая мероприятие. </summary>
    public class EventEntity
    {
        /// <summary> Уникальный идентификатор мероприятия. </summary>
        public Guid EventId { get; set; }

        /// <summary> Название (заголовок) мероприятия. </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary> Описание мероприятия. </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary> Количество человек отметивших мероприятие (дали отклик на событие). </summary>
        public long Responses { get; set; }

        /// <summary> Дата проведения мероприятия. </summary>
        public DateTime EventDate { get; set; }

        /// <summary> Статус проведения мероприятия. </summary>
        public StatusEnum Status { get; set; }
    }
}

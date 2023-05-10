using Event.Domain.Enums;

namespace Event.Domain.Entities
{
    public class EventEntity
    {
        public Guid EventId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        /// <summary> Количество человек отметивших мероприятие (дали отклик на событие). </summary>
        public long Responses { get; set; }

        /// <summary> Дата проведения мероприятия. </summary>
        public DateTime EventDate { get; set; }

        /// <summary> Статус проведения мероприятия. </summary>
        public StatusType Status { get; set; }

        public string Address { get; set; } = string.Empty;

        public Guid AccountId { get; set; }

        /// <summary> Поле для связи One to many. </summary>
        public AccountEntity? Account { get; set; }

        /// <summary> Поле для связи Many to many. </summary>
        public ICollection<AccountEntity>? Accounts { get; set;}
    }
}

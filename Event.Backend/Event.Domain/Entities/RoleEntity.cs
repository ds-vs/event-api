namespace Event.Domain.Entities
{
    /// <summary> Сущность описывающая роль пользователя. </summary>
    public class RoleEntity
    {
        /// <summary> Уникальный идентификатор роли. </summary>
        public int RoleId { get; set; }

        /// <summary> Наименование роли. </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary> Описание роли. </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary> Поле для связи One to many. </summary>
        public ICollection<AccountEntity> Accounts { get; set; }
    }
}

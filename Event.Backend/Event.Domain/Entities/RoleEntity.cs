namespace Event.Domain.Entities
{
    public class RoleEntity
    {
        public int RoleId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        /// <summary> Поле для связи One to many. </summary>
        public ICollection<AccountEntity> Accounts { get; set; }
    }
}

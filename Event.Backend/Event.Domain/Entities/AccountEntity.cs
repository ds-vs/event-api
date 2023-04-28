namespace Event.Domain.Entities
{
    /// <summary> Сущность описывающая данные о пользователе. </summary>
    public class AccountEntity
    {
        public Guid AccountId { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}

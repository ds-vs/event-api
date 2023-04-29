namespace Event.Domain.Entities
{
    /// <summary> Сущность описывающая данные о пользователе. </summary>
    public class AccountEntity
    {
        /// <summary> Уникальный идентификатор пользователя. </summary>
        public Guid AccountId { get; set; }

        /// <summary> Логин пользователя. </summary>
        public string Login { get; set; } = string.Empty;

        /// <summary> Электронный почтовый адрес пользователя. </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary> Строка с хэшем пароля. </summary>
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary> Идентификатор роли пользователя. </summary>
        public int RoleId { get; set; }

        /// <summary> Поле для связи One to many. </summary>
        public RoleEntity Role { get; set; }

        public string? RefreshToken { get; set; } = string.Empty;
        public DateTime? TokenCreated { get; set; }
        public DateTime? TokenExpires { get; set; }
    }
}

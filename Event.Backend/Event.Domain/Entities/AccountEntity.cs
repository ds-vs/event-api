namespace Event.Domain.Entities
{
    public class AccountEntity
    {
        public Guid AccountId { get; set; }

        public string Login { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public int RoleId { get; set; }

        public RoleEntity Role { get; set; }

        public string? RefreshToken { get; set; } = string.Empty;
        public DateTime? TokenCreated { get; set; }
        public DateTime? TokenExpires { get; set; }

        public ICollection<EventEntity>? Events { get; set; }
        public ICollection<EventEntity>? EventsToAccounts { get; set; }

    }
}

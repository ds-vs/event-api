namespace Event.Domain.Dto.Account
{
    public class TokenDto
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime? TokenExpires { get; set; }
    }
}

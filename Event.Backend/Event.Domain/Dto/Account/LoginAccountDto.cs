using System.ComponentModel.DataAnnotations;

namespace Event.Domain.Dto.Account
{
    public class LoginAccountDto
    {
        [Required]
        public string Login { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}

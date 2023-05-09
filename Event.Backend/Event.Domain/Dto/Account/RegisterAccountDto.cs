using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Event.Domain.Dto.Account
{
    public class RegisterAccountDto
    {
        [Required]
        public string Login { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [DefaultValue(1)]
        public int RoleId { get; set; }
    }
}

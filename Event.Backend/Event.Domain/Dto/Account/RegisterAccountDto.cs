﻿namespace Event.Domain.Dto.Account
{
    public class RegisterAccountDto
    {
        public string Login { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; } = 1;
    }
}
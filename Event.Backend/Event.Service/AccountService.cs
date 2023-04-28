using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Event.Domain;
using Event.Domain.Dto.Account;
using Event.Domain.Entities;
using Event.Domain.Repositories;
using Event.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Event.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;

        public AccountService(IAccountRepository accountRepository, IConfiguration configuration)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
        }

        public async Task<IResponse<RegisterAccountDto>> RegisterAsync(RegisterAccountDto account)
        {
            try
            {
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(account.Password);

                var accountEntity = new AccountEntity()
                {
                    Login = account.Login,
                    Email = account.Email,
                    PasswordHash = passwordHash,
                };

                await _accountRepository.CreateAsync(accountEntity);

                return new Response<RegisterAccountDto>()
                {
                    Description = "Account created successfully.",
                    Status = HttpStatusCode.OK,
                };
            }
            catch (Exception e)
            {
                return new Response<RegisterAccountDto>()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Description = $"[RegisterAsync] : {e.Message}",
                };
            }
        }

        public async Task<IResponse<string>> LoginAsync(LoginAccountDto account)
        {
            try
            {
                var entity = await _accountRepository.GetAsync(account.Login);

                if (entity == null)
                {
                    return new Response<string>()
                    {
                        Description = "Account not found.",
                        Status = HttpStatusCode.NotFound,
                    };
                }

                if (!BCrypt.Net.BCrypt.Verify(account.Password, entity.PasswordHash))
                {
                    return new Response<string>()
                    {
                        Description = "Wrong password.",
                        Status = HttpStatusCode.NotFound,
                    };
                }

                string token = CreateToken(entity);

                return new Response<string>()
                {
                    Data = token,
                    Description = "Success.",
                    Status = HttpStatusCode.OK,
                };
            }
            catch (Exception e)
            {
                return new Response<string>()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Description = $"[LoginAsync] : {e.Message}",
                };
            }
        }

        private string CreateToken(AccountEntity account)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.Login),
                new Claim(ClaimTypes.Email, account.Email),
            };

            var key = new SymmetricSecurityKey
                (
                    Encoding.UTF8.GetBytes
                    (
                        _configuration.GetSection("AppSettings:Token").Value!
                    )
                );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken
                (
                    claims: claims,
                    expires: DateTime.Now.AddDays(3),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}

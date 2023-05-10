using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
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
            _accountRepository= accountRepository;
            _configuration = configuration;
        }

        public async Task<IResponse<RegisterAccountDto>> RegisterAsync(RegisterAccountDto account)
        {
            try
            {
                var duplicateAccount = await _accountRepository.GetAsync(account.Login);

                if (duplicateAccount == null)
                {
                    var passwordHash = BCrypt.Net.BCrypt.HashPassword(account.Password);

                    var accountEntity = new AccountEntity()
                    {
                        Login = account.Login,
                        Email = account.Email,
                        PasswordHash = passwordHash,
                        RoleId = account.RoleId,
                    };

                    await _accountRepository.CreateAsync(accountEntity);

                    return new Response<RegisterAccountDto>()
                    {
                        Description = "Account created successfully.",
                        Status = HttpStatusCode.OK,
                    };
                }
                else
                {
                    return new Response<RegisterAccountDto>()
                    {
                        Description = "Account exist.",
                        Status = HttpStatusCode.OK,
                    };
                }
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

        public async Task<IResponse<TokenDto>> LoginAsync(LoginAccountDto account)
        {
            try
            {
                var entity = await _accountRepository.GetAsync(account.Login);

                if (entity == null)
                {
                    return new Response<TokenDto>()
                    {
                        Description = "Account not found.",
                        Status = HttpStatusCode.NotFound,
                    };
                }

                if (!BCrypt.Net.BCrypt.Verify(account.Password, entity.PasswordHash))
                {
                    return new Response<TokenDto>()
                    {
                        Description = "Wrong password.",
                        Status = HttpStatusCode.NotFound,
                    };
                }

                string token = CreateToken(entity);

                var refreshToken = GenerateRefreshToken();

                entity.RefreshToken = refreshToken.Token;
                entity.TokenCreated = refreshToken.Created;
                entity.TokenExpires = refreshToken.Expires;

                await _accountRepository.UpdateAsync(entity);

                return new Response<TokenDto>()
                {
                    Data = new TokenDto()
                    {
                        Token = token,
                        RefreshToken = refreshToken.Token,
                        TokenExpires = refreshToken.Expires
                    },
                    Description = "Success.",
                    Status = HttpStatusCode.OK,
                };
            }
            catch (Exception e)
            {
                return new Response<TokenDto>()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Description = $"[LoginAsync] : {e.Message}",
                };
            }
        }

        public async Task<IResponse<TokenDto>> GetRefreshTokenAsync(string login)
        {
            try
            {
                var entity = await _accountRepository.GetAsync(login);

                if (entity == null)
                {
                    return new Response<TokenDto>()
                    {
                        Description = "Account not found.",
                        Status = HttpStatusCode.NotFound,
                    };
                }

                return new Response<TokenDto>()
                {
                    Data = new TokenDto()
                    {
                        RefreshToken = entity.RefreshToken!,
                        TokenExpires = entity.TokenExpires,
                    },
                    Description = "Success.",
                    Status = HttpStatusCode.OK,
                };
            }
            catch (Exception e)
            {
                return new Response<TokenDto>()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Description = $"[RefreshTokenAsync] : {e.Message}",
                };
            }
        }

        public async Task<IResponse<TokenDto>> NewRefreshTokenAsync(string login)
        {
            try
            {
                var entity = await _accountRepository.GetAsync(login);

                if (entity == null)
                {
                    return new Response<TokenDto>()
                    {
                        Description = "Account not found.",
                        Status = HttpStatusCode.NotFound,
                    };
                }

                string token = CreateToken(entity);

                var newRefreshToken = GenerateRefreshToken();

                entity.RefreshToken = newRefreshToken.Token;
                entity.TokenCreated = newRefreshToken.Created;
                entity.TokenExpires = newRefreshToken.Expires;

                await _accountRepository.UpdateAsync(entity);

                return new Response<TokenDto>()
                {
                    Data = new TokenDto()
                    {
                        Token = token,
                        RefreshToken = newRefreshToken.Token,
                        TokenExpires = newRefreshToken.Expires,
                    },
                    Description = "Success.",
                    Status = HttpStatusCode.OK,
                };
            }
            catch (Exception e)
            {
                return new Response<TokenDto>()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Description = $"[NewRefreshTokenAsync] : {e.Message}",
                };
            }
        }

        private string CreateToken(AccountEntity account)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.Login),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim(ClaimTypes.Role, account.Role.Name)
            };

            var key = new SymmetricSecurityKey
                (
                    Encoding.UTF8.GetBytes
                    (
                        _configuration.GetSection("Jwt:SecretKey").Value!
                    )
                );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken
                (
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
            };

            return refreshToken;
        }

    }
}

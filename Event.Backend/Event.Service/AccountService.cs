using System.Net;
using Event.Domain;
using Event.Domain.Dto.Account;
using Event.Domain.Entities;
using Event.Domain.Repositories;
using Event.Service.Interfaces;

namespace Event.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<IResponse<AccountDto>> RegisterAsync(AccountDto account)
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

                return new Response<AccountDto>()
                {
                    Status = HttpStatusCode.OK,
                };
            }
            catch (Exception e)
            {
                return new Response<AccountDto>()
                {
                    Status = HttpStatusCode.InternalServerError,
                    Description = $"[RegisterAsync] : {e.Message}",
                };
            }
        }
    }
}

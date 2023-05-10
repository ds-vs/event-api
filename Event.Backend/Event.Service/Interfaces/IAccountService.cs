using Event.Domain;
using Event.Domain.Dto.Account;

namespace Event.Service.Interfaces
{
    public interface IAccountService
    {
        Task<IResponse<RegisterAccountDto>> RegisterAsync(RegisterAccountDto account);
        Task<IResponse<TokenDto>> LoginAsync(LoginAccountDto account);
        Task<IResponse<TokenDto>> GetRefreshTokenAsync(string login);
        Task<IResponse<TokenDto>> NewRefreshTokenAsync(string login);
    }
}

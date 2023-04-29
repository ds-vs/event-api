using Event.Domain;
using Event.Domain.Dto.Account;
using Event.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Event.Service.Interfaces
{
    /// <summary> Сервис отвечающий за бизнес логику, связанную c учетными данными пользователей. </summary>
    public interface IAccountService
    {
        Task<IResponse<RegisterAccountDto>> RegisterAsync(RegisterAccountDto account);
        Task<IResponse<TokenDto>> LoginAsync(LoginAccountDto account);
        Task<IResponse<TokenDto>> GetRefreshTokenAsync(string login);
        Task<IResponse<TokenDto>> NewRefreshTokenAsync(string login);
    }
}

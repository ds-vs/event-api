using Event.Domain;
using Event.Domain.Dto.Account;

namespace Event.Service.Interfaces
{
    /// <summary> Сервис отвечающий за бизнес логику, связанную c учетными данными пользователей. </summary>
    public interface IAccountService
    {
        Task<IResponse<RegisterAccountDto>> RegisterAsync(RegisterAccountDto account);
        Task<IResponse<string>> LoginAsync(LoginAccountDto account);
    }
}

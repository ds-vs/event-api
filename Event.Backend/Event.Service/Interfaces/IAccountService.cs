using Event.Domain;
using Event.Domain.Dto.Account;
using Event.Domain.Entities;

namespace Event.Service.Interfaces
{
    /// <summary> Сервис отвечающий за бизнес логику, связанную c учетными данными пользователей. </summary>
    public interface IAccountService
    {
        Task<IResponse<AccountDto>> RegisterAsync(AccountDto account);
    }
}

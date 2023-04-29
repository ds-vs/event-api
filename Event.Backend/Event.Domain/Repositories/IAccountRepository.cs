using Event.Domain.Entities;

namespace Event.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<AccountEntity> CreateAsync(AccountEntity entity);
        Task<AccountEntity> GetAsync(string login);
        Task UpdateAsync(AccountEntity entity);
    }
}

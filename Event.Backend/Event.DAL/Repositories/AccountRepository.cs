using Event.Domain.Entities;
using Event.Domain.Repositories;

namespace Event.DAL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly EventDbContext _context;

        public AccountRepository(EventDbContext context)
        {
            _context = context;
        }

        public async Task<AccountEntity> CreateAsync(AccountEntity entity)
        {
            _context.Account.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}

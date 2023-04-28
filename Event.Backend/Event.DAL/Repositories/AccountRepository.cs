using Event.Domain.Entities;
using Event.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

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

        public async Task<AccountEntity> GetAsync(string login)
        {
            var entity = await _context.Account
                .Include(x => x.Role)
                .FirstOrDefaultAsync(entity => entity.Login == login);

            return entity!;
        }
    }
}

using bms.Data;
using bms.Models;
using bms.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bms.Repositories.Implementations
{
    public class MemberAccountRepository : IMemberAccountRepository
    {
        private readonly bms.Data.BmsDbContext _context;
        
        public MemberAccountRepository(bms.Data.BmsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }

    public async Task<IEnumerable<Account>> GetAllByMemberIdAsync(int memberId)
    {
        var memberAccounts= await  _context.Accounts.Where(account=>account.MemberId==memberId).ToListAsync();
        return memberAccounts;
    }

    public async Task<Account?> GetByIdAsync(int accountId)
    {
       return await  _context.Accounts.FindAsync(accountId);
    }

        public async Task UpdateAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }
    }
}
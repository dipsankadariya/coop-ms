
using bms.Data;
using bms.Models;
using Microsoft.EntityFrameworkCore;

public class AccountTransactionRepository : IAccountTransactionRepository
{
    private readonly bms.Data.BmsDbContext _context;

    public AccountTransactionRepository(bms.Data.BmsDbContext context)
    {
        _context = context;
    }

    public async Task AddTransactionAsync(Transaction accountTransaction)
    {
        await _context.Transactions.AddAsync(accountTransaction);
         await  _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Transaction>> GetAllTransactionsByAccountIdAsync(int accountId)
    {
        return  await _context.Transactions.Where(transaction => transaction.AccountId == accountId).ToListAsync();
    }
}
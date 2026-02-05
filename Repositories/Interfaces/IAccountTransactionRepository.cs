using bms.Models;

public interface IAccountTransactionRepository
{
    // Define methods for account transaction operations
    Task AddTransactionAsync(Transaction accountTransaction);

    Task<IEnumerable<Transaction>> GetAllTransactionsByAccountIdAsync(int accountId);


}
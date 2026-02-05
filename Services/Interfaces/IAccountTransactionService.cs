public interface IAccountTransactionService
{
    // Define methods for account transaction operations
    Task AddTransactionAsync(AccountTransactionDto accountTransactionDto);

    Task<IEnumerable<AccountTransactionDto>> GetAllTransactionsByAccountIdAsync(int accountId);
}

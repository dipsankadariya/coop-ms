public interface IAccountTransactionService
{
    // Define methods for account transaction operations
    Task AddTransactionAsync(AccountTransactionDto accountTransactionDto);
}

using bms.Models;namespace bms.Mappers
{
    public static class AccountTransactionMapper
    {
        public static AccountTransactionDto MapToDto(Transaction accountTransaction) => new AccountTransactionDto
        {
            TransactionId = accountTransaction.TransactionId,
            AccountId = accountTransaction.AccountId,
            TransactionType = accountTransaction.TransactionType,
            Amount = accountTransaction.Amount,
            TransactionDate = accountTransaction.TransactionDate,
            BalanceAfter = accountTransaction.BalanceAfter,
            Notes = accountTransaction.Notes
        };

        public static Transaction MapToEntity(AccountTransactionDto accountTransactionDto) => new Transaction
        {
            AccountId = accountTransactionDto.AccountId,
            TransactionType = accountTransactionDto.TransactionType,
            Amount = accountTransactionDto.Amount,
            TransactionDate = accountTransactionDto.TransactionDate,
            BalanceAfter = accountTransactionDto.BalanceAfter,
            Notes = accountTransactionDto.Notes
        };
    }
}

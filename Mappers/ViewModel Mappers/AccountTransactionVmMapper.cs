using bms.Models;
using bms.ViewModels;
using bms.Mappers; // Add this at the top of AccountTransactionController.cs

namespace bms.Mappers
{
public static class AccountTransactionVmMapper
{
    public static AccountTransactionDto MapVmToDto(AccountTransactionVm accountTransactionVm)=>new AccountTransactionDto
    {
        TransactionId=accountTransactionVm.TransactionId,
        AccountId=accountTransactionVm.AccountId,
        TransactionType=accountTransactionVm.TransactionType,
        Amount=accountTransactionVm.Amount,
        TransactionDate=accountTransactionVm.TransactionDate,
        BalanceAfter=accountTransactionVm.BalanceAfter,
        Notes=accountTransactionVm.Notes
    };
    public static AccountTransactionVm MapDtoToVm(AccountTransactionDto accountTransactionDto)=>new AccountTransactionVm
    {
        TransactionId=accountTransactionDto.TransactionId,
        AccountId=accountTransactionDto.AccountId,
        TransactionType=accountTransactionDto.TransactionType,
        Amount=accountTransactionDto.Amount,
        TransactionDate=accountTransactionDto.TransactionDate,
        BalanceAfter=accountTransactionDto.BalanceAfter,
        Notes=accountTransactionDto.Notes
    };
}
}
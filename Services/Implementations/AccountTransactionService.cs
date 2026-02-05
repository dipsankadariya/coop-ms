
using bms.Models;
using bms.Repositories.Interfaces;
using bms.Mappers;

public class AccountTransactionService : IAccountTransactionService
{
    private readonly IAccountTransactionRepository _accountTransactionRepository;
    private readonly IMemberAccountRepository _memberAccountRepository;


    public AccountTransactionService(IAccountTransactionRepository accountTransactionRepository, IMemberAccountRepository memberAccountRepository)
    {
        _accountTransactionRepository = accountTransactionRepository;
        _memberAccountRepository = memberAccountRepository;
    }
    public async Task AddTransactionAsync(AccountTransactionDto accountTransactionDto)
    {
     var account= await _memberAccountRepository.GetByIdAsync(accountTransactionDto.AccountId);

        if (account == null)
        {
            throw new Exception("Account not found");
        }
      
      decimal oldBalance= account.Balance;

      decimal newBalance;

        if (accountTransactionDto.TransactionType == "Deposit")
        {
            newBalance = oldBalance + accountTransactionDto.Amount;
        }
        else if (accountTransactionDto.TransactionType == "Withdrawal")
        {
            if (oldBalance < accountTransactionDto.Amount)
            {
                throw new Exception("Insufficient funds for withdrawal");
            }
            newBalance = oldBalance - accountTransactionDto.Amount;
        }
        else
        {
            throw new Exception("Invalid transaction type");
        }

        //map to entity
        var entity= AccountTransactionMapper.MapToEntity(accountTransactionDto);
        //set the balance after
        entity.BalanceAfter=newBalance;
        entity.TransactionDate=DateTime.Now;

        //update the account balance
        account.Balance = newBalance;

       //add  transaction to  the context,not saving yet.

        await _accountTransactionRepository.AddTransactionAsync(entity);

        await _memberAccountRepository.UpdateAsync(account);
    }

    public async Task<IEnumerable<AccountTransactionDto>> GetAllTransactionsByAccountIdAsync(int accountId)
    {
        var account= await _memberAccountRepository.GetByIdAsync(accountId);
        if (account == null)
        {
            throw new Exception("Account not found");
        }

        var transactions= await _accountTransactionRepository.GetAllTransactionsByAccountIdAsync(accountId);
        var transactionssDto= transactions.Select(AccountTransactionMapper.MapToDto).ToList();
        return transactionssDto;
    }
}
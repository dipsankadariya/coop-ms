using System.Threading.Tasks;
using bms.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using bms.ViewModels; 
using bms.Mappers; // Needed for AccountTransactionVmMapper

public class AccountTransactionController : Controller
{
    private readonly IAccountTransactionService _accountTransactionService;
    private readonly IMemberAccountService _memberAccountService;

    public AccountTransactionController(IAccountTransactionService accountTransactionService, IMemberAccountService memberAccountService)
    {
        _accountTransactionService = accountTransactionService;
        _memberAccountService = memberAccountService;
    }

    //get-add transaction
    public async Task<IActionResult> AddTransaction(int accountId)
    {
        var account = await _memberAccountService.GetAccountByIdAsync(accountId);
        if (account == null)
        {
            return NotFound("Account not found");
        }

        var vm = new AccountTransactionVm
        {
            AccountId = accountId,
            Amount = 0,
            MemberName = account.MemberName
        };

        return View(vm);
    }

  //post-add transaction
    [HttpPost]
    public async Task<IActionResult> AddTransaction(AccountTransactionVm accountTransactionVm)
    {
        if (!ModelState.IsValid)
        {
            return View(accountTransactionVm);
        }

        try
        {
            var accountTransactionDto = AccountTransactionVmMapper.MapVmToDto(accountTransactionVm);
            await _accountTransactionService.AddTransactionAsync(accountTransactionDto);
           
            TempData["SuccessMessage"] = "Transaction added successfully!";

            return RedirectToAction("ViewStatement", "AccountTransaction", new { accountId = accountTransactionVm.AccountId });
        }
        catch (Exception ex)
            {
    // Log the inner exception - that's where the real error is
    var innerMessage = ex.InnerException?.Message ?? ex.Message;
    Console.WriteLine($"ERROR: {innerMessage}");
    Console.WriteLine($"FULL EXCEPTION: {ex}");
    
    // Repopulate MemberName before returning view on error
    var account = await _memberAccountService.GetAccountByIdAsync(accountTransactionVm.AccountId);
    if (account != null)
    {
        accountTransactionVm.MemberName = account.MemberName;
    }
    
    TempData["ErrorMessage"] = innerMessage; // Show the real error
    return View(accountTransactionVm);
}

    }

    //get view statement
    public async Task<IActionResult> ViewStatement(int accountId)
    {
        var account = await _memberAccountService.GetAccountByIdAsync(accountId);
        if (account == null)
        {
            return NotFound("Account not found");
        }

        // Logic to get transactions and pass to view can
        var transactions= await _accountTransactionService.GetAllTransactionsByAccountIdAsync(accountId);
        var transactionVms= transactions.Select(transaction=> AccountTransactionVmMapper.MapDtoToVm(transaction)).ToList();
        ViewBag.MemberName= account.MemberName;
        return View(transactionVms);
        

        
    }
}
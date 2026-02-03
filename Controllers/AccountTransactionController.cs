using System.Threading.Tasks;
using bms.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

public class AccountTransactionController : Controller
{
    private readonly IAccountTransactionService _accountTransactionService;
    private readonly IMemberAccountService _memberAccountService;

    public AccountTransactionController(IAccountTransactionService accountTransactionService, IMemberAccountService memberAccountService)
    {
        _accountTransactionService = accountTransactionService;
        _memberAccountService = memberAccountService;
    }

    //add transaction - get
    public async Task<IActionResult> AddTransaction(int accountId)
    {
        var account= await _memberAccountService.GetAccountByIdAsync(accountId);
        if(account==null)
        {
            return NotFound("Account not found");
        }
        var vm= new AccountTransactionVm
        {
            AccountId=accountId,
            Amount=account.Balance,
            
        };
        return View(vm);
    }

    //add transaction - post
    [HttpPost]
    public async Task<IActionResult> AddTransaction(AccountTransactionVm accountTransactionVm)
    {
        try
        {
             var accountTransactionDto=  AccountTransactionVmMapper.MapVmToDto(accountTransactionVm);
            await _accountTransactionService.AddTransactionAsync(accountTransactionDto);
            TempData["SuccessMessage"]="Transaction added successfully";
            return RedirectToAction("ViewStatement","MemberAccount", new { accountId=accountTransactionVm.AccountId });       
        }
        catch(Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(accountTransactionVm);
        }
    }
}
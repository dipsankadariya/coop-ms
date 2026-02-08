using System.Threading.Tasks;
using bms.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using bms.ViewModels; 
using bms.Mappers;
using Microsoft.AspNetCore.Authorization;

namespace bms.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    public class AccountTransactionController : Controller
    {
        private readonly IAccountTransactionService _accountTransactionService;
        private readonly IMemberAccountService _memberAccountService;
        private readonly IMemberService _memberService;

        public AccountTransactionController(
            IAccountTransactionService accountTransactionService, 
            IMemberAccountService memberAccountService,
            IMemberService memberService)
        {
            _accountTransactionService = accountTransactionService;
            _memberAccountService = memberAccountService;
            _memberService = memberService;
        }

        public async Task<IActionResult> AddTransaction(int? accountId, string? transactionType)
        {
            var members = await _memberService.GetActiveMembersAsync();
            var memberList = members.Select(m => new SelectListItem
            {
                Value = m.MemberId.ToString(),
                Text = m.FullName
            }).ToList();

            var vm = new AccountTransactionVm
            {
                MemberList = memberList,
                AccountList = new List<SelectListItem>(),
                TransactionType = transactionType ?? ""
            };

            if (accountId.HasValue && accountId.Value > 0)
            {
                var account = await _memberAccountService.GetAccountByIdAsync(accountId.Value);
                if (account != null)
                {
                    vm.AccountId = accountId.Value;
                    vm.MemberId = account.MemberId;
                    vm.MemberName = account.MemberName;

                    var accounts = await _memberAccountService.GetAllAccountsByMemberIdAsync(account.MemberId);
                    vm.AccountList = accounts
                        .Where(a => a.Status?.ToLower() == "active")
                        .Select(a => new SelectListItem
                        {
                            Value = a.AccountId.ToString(),
                            Text = $"{a.AccountType} - ₹{a.Balance:N2}",
                            Selected = a.AccountId == accountId.Value
                        }).ToList();

                    vm.MemberList = memberList.Select(m => new SelectListItem
                    {
                        Value = m.Value,
                        Text = m.Text,
                        Selected = m.Value == account.MemberId.ToString()
                    }).ToList();
                }
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> GetAccountsByMember(int memberId)
        {
            var accounts = await _memberAccountService.GetAllAccountsByMemberIdAsync(memberId);
            var activeAccounts = accounts
                .Where(a => a.Status?.ToLower() == "active")
                .Select(a => new { 
                    value = a.AccountId, 
                    text = $"{a.AccountType} - ₹{a.Balance:N2}" 
                });
            
            return Json(activeAccounts);
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction(AccountTransactionVm accountTransactionVm)
        {
            if (!ModelState.IsValid)
            {
                await ReloadDropdowns(accountTransactionVm);
                return View(accountTransactionVm);
            }

            try
            {
                var accountTransactionDto = AccountTransactionVmMapper.MapVmToDto(accountTransactionVm);
                await _accountTransactionService.AddTransactionAsync(accountTransactionDto);
                
                TempData["SuccessMessage"] = "Transaction added successfully!";
                return RedirectToAction("ViewStatement", new { accountId = accountTransactionVm.AccountId });
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine($"ERROR: {innerMessage}");
                
                await ReloadDropdowns(accountTransactionVm);
                TempData["ErrorMessage"] = innerMessage;
                return View(accountTransactionVm);
            }
        }

        private async Task ReloadDropdowns(AccountTransactionVm vm)
        {
            var members = await _memberService.GetActiveMembersAsync();
            vm.MemberList = members.Select(m => new SelectListItem
            {
                Value = m.MemberId.ToString(),
                Text = m.FullName,
                Selected = m.MemberId == vm.MemberId
            }).ToList();

            if (vm.MemberId > 0)
            {
                var accounts = await _memberAccountService.GetAllAccountsByMemberIdAsync(vm.MemberId);
                vm.AccountList = accounts
                    .Where(a => a.Status?.ToLower() == "active")
                    .Select(a => new SelectListItem
                    {
                        Value = a.AccountId.ToString(),
                        Text = $"{a.AccountType} - ₹{a.Balance:N2}",
                        Selected = a.AccountId == vm.AccountId
                    }).ToList();
            }
            else
            {
                vm.AccountList = new List<SelectListItem>();
            }
        }

        public async Task<IActionResult> SelectStatement()
        {
            var members = await _memberService.GetActiveMembersAsync();
            var memberList = members.Select(m => new SelectListItem
            {
                Value = m.MemberId.ToString(),
                Text = m.FullName
            }).ToList();

            var vm = new AccountTransactionVm
            {
                MemberList = memberList,
                AccountList = new List<SelectListItem>()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult SelectStatement(AccountTransactionVm vm)
        {
            if (vm.AccountId <= 0)
            {
                TempData["ErrorMessage"] = "Please select an account.";
                return RedirectToAction("SelectStatement");
            }

            return RedirectToAction("ViewStatement", new { accountId = vm.AccountId });
        }

        public async Task<IActionResult> ViewStatement(int accountId)
        {
            var account = await _memberAccountService.GetAccountByIdAsync(accountId);
            if (account == null)
            {
                return NotFound("Account not found");
            }

            var transactions = await _accountTransactionService.GetAllTransactionsByAccountIdAsync(accountId);
            var transactionVms = transactions.Select(transaction => AccountTransactionVmMapper.MapDtoToVm(transaction)).ToList();
            ViewBag.MemberName = account.MemberName;
            ViewBag.AccountId = accountId;
            ViewBag.AccountType = account.AccountType;
            ViewBag.Balance = account.Balance;
            return View(transactionVms);
        }
    }
}
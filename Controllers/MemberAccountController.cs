using bms.Mappers.ViewModelMappers;
using bms.Services.Interfaces;
using bms.ViewModels.Member;
using bms.ViewModels.MemberAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bms.Controllers
{
      [Authorize(Roles =("Admin,Staff"))]
    public class MemberAccountController : Controller
    {
        private readonly IMemberAccountService _memberAccountService;
        private readonly IMemberService _memberService;
        private readonly IMemberShareService _memberShareService;

        public MemberAccountController(IMemberAccountService memberAccountService, IMemberService memberService, IMemberShareService memberShareService)
        {
            _memberAccountService = memberAccountService;
            _memberService = memberService;
            _memberShareService = memberShareService;
        }

        // GET: MemberAccount/Index
        // Show all active members to choose from
        [HttpGet]

        public async Task<IActionResult> Index()
        {
            TempData.Clear(); // Clear old messages from other controllers
            try
            {
                var membersDtos = await _memberService.GetActiveMembersAsync();
                var membersVms = membersDtos.Select(MemberVmMapper.MapDtoToViewModel).ToList();
                
                // Get share count for each member
                foreach (var memberVm in membersVms)
                {
                    try
                    {
                        var totalShares = await _memberShareService.GetTotalShareByMemberIdAsync(memberVm.MemberId);
                        memberVm.ShareCount = totalShares > 0 ? 1 : 0;
                    }
                    catch
                    {
                        // If error getting shares, assume member has no shares
                        memberVm.ShareCount = 0;
                    }
                }
                
                return View(membersVms);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while loading members: " + ex.Message;
                return View(new List<MemberVm>());
            }
        }

        // GET: MemberAccount/Create?memberId=1
        // Show create account form for a member
        [HttpGet]
        public async Task<IActionResult> Create(int memberId)
        {
            try
            {
                var memberDto = await _memberService.GetMemberByIdAsync(memberId);
                if (memberDto == null)
                {
                    TempData["ErrorMessage"] = "Member not found.";
                    return RedirectToAction("Index");
                }

                // Send member info via ViewBag (for display only)
                var memberVm = MemberVmMapper.MapDtoToViewModel(memberDto);
                ViewBag.Member = memberVm;

                // Main model = empty account form
                var accountVm = new MemberAccountVm
                {
                    MemberId = memberId
                };

                return View(accountVm);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while preparing the account creation form: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // POST: MemberAccount/Create
        // Create new account for member
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MemberAccountVm memberAccountVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var memberAccountDto = MemberAccountVmMapper.MapVmToDto(memberAccountVm);
                    await _memberAccountService.AddMemberAccountAsync(memberAccountDto);

                    TempData["SuccessMessage"] = "Member account created successfully.";
                    return RedirectToAction("ViewAccounts", new { memberId = memberAccountVm.MemberId });
                }

                // Reload member info again for the view
                var memberDto = await _memberService.GetMemberByIdAsync(memberAccountVm.MemberId);
                ViewBag.Member = MemberVmMapper.MapDtoToViewModel(memberDto);

                return View(memberAccountVm);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while creating the member account: " + ex.Message;

                var memberDto = await _memberService.GetMemberByIdAsync(memberAccountVm.MemberId);
                ViewBag.Member = MemberVmMapper.MapDtoToViewModel(memberDto);

                return View(memberAccountVm);
            }
        }

        // GET: MemberAccount/ViewAccounts?memberId=1
        // Show all accounts of a member
        [HttpGet]
        public async Task<IActionResult> ViewAccounts(int memberId)
        {
            try
            {
                var memberDto = await _memberService.GetMemberByIdAsync(memberId);
                if (memberDto == null)
                {
                    TempData["ErrorMessage"] = "Member not found.";
                    return RedirectToAction("Index");
                }

                var accountDtos = await _memberAccountService.GetAllAccountsByMemberIdAsync(memberId);
                var accountVms = accountDtos.Select(MemberAccountVmMapper.MapDtoToVm).ToList();

                // Send member info to view
                ViewBag.Member = MemberVmMapper.MapDtoToViewModel(memberDto);

                return View(accountVms);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while fetching accounts: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // GET: MemberAccount/Update?accountId=1
        // Show update form (only status change)
        [HttpGet]
        public async Task<IActionResult> Update(int accountId)
        {
            try
            {
                var accountDto = await _memberAccountService.GetAccountByIdAsync(accountId);
                if (accountDto == null)
                {
                    TempData["ErrorMessage"] = "Account not found.";
                    return RedirectToAction("Index");
                }

                // Load member info
                var memberDto = await _memberService.GetMemberByIdAsync(accountDto.MemberId);
                ViewBag.Member = MemberVmMapper.MapDtoToViewModel(memberDto);

                var accountVm = MemberAccountVmMapper.MapDtoToVm(accountDto);
                return View(accountVm);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while loading the update form: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // POST: MemberAccount/Update
        // Update account status only
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(MemberAccountVm memberAccountVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var accountDto = MemberAccountVmMapper.MapVmToDto(memberAccountVm);
                    await _memberAccountService.UpdateMemberAccountAsync(accountDto);

                    TempData["SuccessMessage"] = "Account updated successfully.";
                    return RedirectToAction("ViewAccounts", new { memberId = memberAccountVm.MemberId });
                }

                // Reload member info if validation fails
                var memberDto = await _memberService.GetMemberByIdAsync(memberAccountVm.MemberId);
                ViewBag.Member = MemberVmMapper.MapDtoToViewModel(memberDto);

                return View(memberAccountVm);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while updating the account: " + ex.Message;

                var memberDto = await _memberService.GetMemberByIdAsync(memberAccountVm.MemberId);
                ViewBag.Member = MemberVmMapper.MapDtoToViewModel(memberDto);

                return View(memberAccountVm);
            }
        }
    }
}
  
using bms.Models;
using bms.Repositories.Interfaces;
using bms.Repository.Interfaces;
using bms.Services.Implementations;
using bms.Services.Interfaces;
using bms.ViewModels.Member;
using bms.ViewModels.MemberShare;
using bms.Mappers.ViewModelMappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using bms.Data.DTOs;

namespace bms.Controllers
{
    [Authorize(Roles ="Admin,Staff")]
    public class MemberShareController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly IMemberShareService _memberShareService;

        public MemberShareController(IMemberService memberService, IMemberShareService memberShareService)
        {
            _memberService = memberService;
            _memberShareService = memberShareService;
        }

        //list all the members
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            TempData.Clear();
            try
            {
                var memberDto = await _memberService.GetActiveMembersAsync();
                var memberVm = memberDto.Select(MemberVmMapper.MapDtoToViewModel).ToList();
                return View(memberVm);
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while loading members: " + ex.Message;
                return View(new List<MemberVm>());
            }
        }

        //get: add share form
        [HttpGet]
        public async Task<IActionResult> AddShare(string shareType="Ordinary")
        {
            try
            {
                var activeMembersdto = await _memberService.GetActiveMembersAsync();
                var activeMembers = activeMembersdto.Select(MemberVmMapper.MapDtoToViewModel).ToList();

                var model = new MemberShareVm
                {
                    MemberList = activeMembers,
                    ShareType = shareType
                };
                return View(model);
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while loading the add share form: " + ex.Message;
                return RedirectToAction("Index", "MemberShare");
            }
        }

        //add share post  👉 MODELSTATE REMOVED HERE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddShare(MemberShareVm memberShareVm)
        {
            try
            {
                var memberShareDto = MemberShareVmMapper.MapVmToDto(memberShareVm);
                await _memberShareService.AddMemberShareAsync(memberShareDto);

                TempData["SuccessMessage"] = "Share added successfully.";
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while adding share: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        //get total share for member
        [HttpGet]
        public async Task<IActionResult> GetTotalShareForMember(int memberId)
        {
            try
            {
                var totalShare = await _memberShareService.GetTotalShareByMemberIdAsync(memberId);
                return View(totalShare);
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while fetching total shares: " + ex.Message;
                return RedirectToAction("Index", "MemberShare");
            }
        }

        //View Member share details form page - GET
        [HttpGet]
        public async Task<IActionResult> ViewMemberSharesDetails()
        {
            try
            {
                var activeMembersdto = await _memberService.GetActiveMembersAsync();
                var activeMembers = activeMembersdto.Select(MemberVmMapper.MapDtoToViewModel).ToList();

                var model = new MemberShareVm
                {
                    MemberList = activeMembers
                };
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while loading the form: " + ex.Message;
                return RedirectToAction("Index", "MemberShare");
            }
        }

        //View Member share details form page - POST 👉 MODELSTATE REMOVED HERE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ViewMemberSharesDetails(MemberShareVm memberShareVm)
        {
            try
            {
                if (memberShareVm.MemberId > 0)
                {
                    return RedirectToAction(
                        "GetMemberSharesDetailsbyId",
                        new { memberId = memberShareVm.MemberId }
                    );
                }

                TempData["ErrorMessage"] = "Invalid data provided.";
                return RedirectToAction("Index", "MemberShare");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while processing your request: " + ex.Message;
                return RedirectToAction("Index", "MemberShare");
            }
        }

        //Get member share details by Id
        [HttpGet]
        public async Task<IActionResult> GetMemberSharesDetails(int memberId)
        {
            try
            {
                var memberShares =
                    await _memberShareService.GetAllMemberSharesByMemberIdAsync(memberId);

                var vm = new MemberShareVm
                {
                    MemberShares = memberShares
                        .Select(MemberShareVmMapper.MapDtoToVm)
                        .ToList(),
                    MemberId = memberId
                };
                return View(vm);
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] =
                    "An error occurred while fetching share details: " + ex.Message;

                return RedirectToAction("Index", "MemberShare");
            }
        }
    }
}

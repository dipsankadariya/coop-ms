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
    [Authorize]
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
        public async Task<IActionResult> Index(){
            TempData.Clear(); // Clear old messages from other controllers
            try
            {
             var memberDto= await _memberService.GetAllMembersAsync();
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
        public async Task<IActionResult> AddShare(int  memberId){
          try{
            var memberDto= await _memberService.GetMemberByIdAsync(memberId);
            if(memberDto==null){
                return NotFound();
            }
          var memberVm= MemberVmMapper.MapDtoToViewModel(memberDto);
          return View(memberVm);
          }
          catch(Exception ex){
            TempData["ErrorMessage"]= "An error occurred while loading  form: " + ex.Message;
            return RedirectToAction("Index", "MemberShare");
        }
        }
    

        //add share post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddShare(MemberShareVm memberShareVm)
        {
           try{
        if (ModelState.IsValid)
        {
          var memberShareDto= MemberShareVmMapper.MapVmToDto(memberShareVm);
           await _memberShareService.AddMemberShareAsync(memberShareDto);
            TempData["SuccessMessage"] = "Share added successfully.";
            return RedirectToAction("Index");
        }
            // Validation failed
            TempData["ErrorMessage"] = "Invalid data provided.";
            return RedirectToAction("Index");
           }
           catch(Exception ex){
            TempData["ErrorMessage"]= "An error occurred while adding share: " + ex.Message;
            return RedirectToAction("Index");
           }
        }

        //get total share for  memeber
        [HttpGet]
        public async Task<IActionResult> GetTotalShareForMember(int memberId)
        {
          try{
            var totalShare= await _memberShareService.GetTotalShareByMemberIdAsync(memberId);
            return View(totalShare);
          }
          catch(Exception ex){
            TempData["ErrorMessage"]= "An error occurred while fetching total shares: " + ex.Message;
            return RedirectToAction("Index", "MemberShare");
          }
        }

        //view all shares details for member
        [HttpGet]
        public async Task<IActionResult> ViewMemberSharesDetails(int memberId)
        {
          try{
            var memberShares= await _memberShareService.GetAllMemberSharesByMemberIdAsync(memberId);
            return View(memberShares);
          }
          catch(Exception ex){
            TempData["ErrorMessage"]= "An error occurred while fetching share details: " + ex.Message;
            return RedirectToAction("Index", "MemberShare");
          }
        }
    }
}
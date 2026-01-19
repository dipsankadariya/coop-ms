using bms.Models;
using bms.Repositories.Interfaces;
using bms.Repository.Interfaces;
using bms.Services.Implementations;
using bms.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace bms.Controllers
{
    public class MemberShareController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly IMemberShareService _memberShareService;

        public MemberShareController(IMemberService memberService, IMemberShareService memberShareService)
        {
            _memberService = memberService;
            _memberShareService = memberShareService;
        }

        public async Task<IActionResult> Index(){
            try
            {
                var members = await _memberService.GetAllMembersAsync();
                return View(members);
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while loading members: " + ex.Message;
                return View(new List<Member>());
            }
        }

        //add share get form
        public async Task<IActionResult> AddShare(int memberId){
          try{
            var member= await _memberService.GetMemberByIdAsync(memberId);
            if(member==null){
                return NotFound();
            }
            return View(member);
          }
          catch(Exception ex){
            TempData["ErrorMessage"]= "An error occurred while loading  form: " + ex.Message;
            return RedirectToAction("Index", "MemberShare");
        }
        }
    

        //add share post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddShare(int memberId, decimal shareAmount, string shareType)
        {
           try{
             if(ModelState.IsValid){
                await _memberShareService.AddMemberShareAsync(memberId, shareAmount, shareType);
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
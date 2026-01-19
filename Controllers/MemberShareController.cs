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

        public IActionResult Index(){
            return View();
        }

        //add share get form
        public async Task<IActionResult> AddShare(int memberId){
            var member= await _memberService.GetMemberByIdAsync(memberId);
            if(member==null){
                return NotFound();
            }
            return View(member);
        }

        //add share post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddShare(int memberId, decimal shareAmount, string shareType)
        {
            if(ModelState.IsValid){
                var member= await _memberService.GetMemberByIdAsync(memberId);
                if (member == null)
                {
                    return NotFound();
                }
                await _memberShareService.AddMemberShareAsync(memberId, shareAmount, shareType);
            }
            return RedirectToAction("Index", "Member");
        }

        //get total share for  memeber
        [HttpGet]
        public async Task<IActionResult> GetTotalShareForMember(int memberId)
        {
           var totalShare= await _memberShareService.GetTotalShareByMemberIdAsync(memberId);
            return View(totalShare);
        }
    }
}
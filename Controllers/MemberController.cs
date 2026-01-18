using bms.Models;
using bms.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace bms.Controllers
{
    public class MemberController : Controller
    {

        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
             _memberService = memberService;
        }

        //get: Members 
        public async Task<IActionResult> Index()
        {
            var memberes = await _memberService.GetAllMembersAsync();
            return View(memberes);
        }


        //getT: Members/Create

        public IActionResult Create()
        {
            return View();
        }

        //post:Members/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> Create(Member member)
        {
            if (ModelState.IsValid)
            {
                await _memberService.AddNewMemberAsync(member);
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        //get : member/update

        public async Task<IActionResult> Update(int id)
        {
            var member = await _memberService.GetMemberByIdAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        //pst:memeber/update

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Member member)
        {
            if (ModelState.IsValid) { 
             await _memberService.UpdateMemberAsync(member);
               return RedirectToAction(nameof(Index));
            }

            return View(member);
        }


        //get:delete
        public async Task<IActionResult> Delete(int id)
        {
            var member = await _memberService.GetMemberByIdAsync(id);

            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        //post:delete
        [HttpPost, ActionName("Delete")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
                await _memberService.DeleteMemberAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

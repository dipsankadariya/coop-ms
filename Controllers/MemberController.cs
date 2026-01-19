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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
           try
           {
            var members = await _memberService.GetAllMembersAsync();
            return View(members);
           }
           catch(Exception ex){
            TempData["ErrorMessage"]= "An error occurred while fetching members: " + ex.Message;
            return RedirectToAction("Index");
           }
        }


        //getT: Members/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //post:Members/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> Create(Member member)
        {
           try{
            if (ModelState.IsValid)
            {
                await _memberService.AddNewMemberAsync(member);
                TempData["SuccessMessage"] = "Member added successfully.";
                return RedirectToAction("Index");
            }
            return View(member);
           }
           catch(Exception ex){
            TempData["ErrorMessage"]= "An error occurred while creating member: " + ex.Message;
            return View(member);
           }
        }

        //get : member/update
       [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var member = await _memberService.GetMemberByIdAsync(id);
                if (member == null)
                {
                    return NotFound();
                }

                return View(member);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while fetching member: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        //pst:memeber/update

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Member member)
        {
            try
            {
                if (ModelState.IsValid) { 
                 await _memberService.UpdateMemberAsync(member);
                 TempData["SuccessMessage"] = "Member updated successfully.";
                   return RedirectToAction(nameof(Index));
                }

                return View(member);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while updating member: " + ex.Message;
                return View(member);
            }
        }


        //get:delete
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var member = await _memberService.GetMemberByIdAsync(id);

                if (member == null)
                {
                    return NotFound();
                }
                return View(member);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while fetching member: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        //post:delete
        [HttpPost, ActionName("Delete")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            try
            {
                await _memberService.DeleteMemberAsync(id);
                TempData["SuccessMessage"] = "Member deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting member: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}

using bms.Data.DTOs;
using bms.Models;
using bms.Services.Interfaces;
using bms.ViewModels.Member;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bms.Controllers
{
    [Authorize]
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
           TempData.Clear(); // Clear old messages from other controllers
           try
           {
            var memberDtos = await _memberService.GetAllMembersAsync();
            var memberViewModels = memberDtos.Select(MemberVmMapper.MapDtoToViewModel).ToList();
            return View(memberViewModels);
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
        public  async Task<IActionResult> Create(MemberVm memberVm)
        {
           try{
            if (ModelState.IsValid)
            {
                var memberDto = MemberVmMapper.MapViewModelToDto(memberVm);
                await _memberService.AddNewMemberAsync(memberDto);
                TempData["SuccessMessage"] = "Member added successfully.";
                return RedirectToAction("Index");
            }
            return View(memberVm);
           }
           catch(Exception ex){
            TempData["ErrorMessage"]= "An error occurred while creating member: " + ex.Message;
            return View(memberVm);
           }
        }

        //get : member/update
       [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var memberDto = await _memberService.GetMemberByIdAsync(id);
                if (memberDto == null)
                {
                    return NotFound();
                }

                var memberVm = MemberVmMapper.MapDtoToViewModel(memberDto);
                return View(memberVm);
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
        public async Task<IActionResult> Update(MemberVm memberVm)
        {
            try
            {
                if (ModelState.IsValid) { 
                 var memberDto = MemberVmMapper.MapViewModelToDto(memberVm);
                 await _memberService.UpdateMemberAsync(memberDto);
                 TempData["SuccessMessage"] = "Member updated successfully.";
                   return RedirectToAction(nameof(Index));
                }

                return View(memberVm);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while updating member: " + ex.Message;
                return View(memberVm);
            }
        }


        //get:delete
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var memberDto = await _memberService.GetMemberByIdAsync(id);

                if (memberDto == null)
                {
                    return NotFound();
                }
                var memberVm = MemberVmMapper.MapDtoToViewModel(memberDto);
                return View(memberVm);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while fetching member: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        //post:delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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

using System.Security.Claims;
using bms.Data.DTOs;
using bms.Services.Interfaces;
using bms.ViewModels.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AdminController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService; 
        }
    
    //get admin/index, show all staff

    [HttpGet]
    public async Task<IActionResult> Users()
    {
        TempData.Clear();

        try
        {
            var users= await _userService.GetAllUsersAsync();
            return View(users);
        }
        catch(Exception ex)
        {
            TempData["ErrorMessage"]= "An error occurred while fetching users: " + ex.Message;
            return View(new List<UserDto>());
    }
    }


    //get: adming/userDetails/{userId} , show details of specific user

    [HttpGet]
    public async Task<IActionResult> UserDetails(int id)
    {
        try
        {
            var user= await _userService.GetUserByIdAsync (id);
            if(user==null)
            {
                TempData["ErrorMessage"]= "User not found.";
                return RedirectToAction("Users");
            }
            return View(user);
        }
        catch(Exception ex)
        {
            TempData["ErrorMessage"]= "An error occurred while fetching user details: " + ex.Message;
            return RedirectToAction("Users");
        }
    }

    //post: admin/toggl status /5, activate or deactivate user

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> ToggleStatus(int id)
     {
        try
        { 
            //User represents the currently logged in user
            //findfirstvalue gets the userId from the cookie,convers it into integer
            var currentUserId= int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            await _userService.ToggleUserStatusAsync(id,currentUserId);
            TempData["SuccessMessage"]= "User status updated successfully.";
            return RedirectToAction("Users");

        }
        catch(Exception ex)
        {
            TempData["ErrorMessage"]= "An error occurred while updating user status: " + ex.Message;
            return RedirectToAction("Users");
        }
     }

     //post:admin/changeRole/userId
     //change  role between  the admin and user
      [HttpPost]
      [ValidateAntiForgeryToken]
    public async  Task<IActionResult> ChangeRole(int id)
      {
        try
        {
              var  currentUserId= int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                  await _userService.ChangeUserRoleAsync(id,currentUserId);
                TempData["SuccessMessage"]= "User role changed successfully.";
                return RedirectToAction("Users");

        }
        catch(Exception ex)
        {
            TempData["ErrorMessage"]= "An error occurred while changing user role: " + ex.Message;
            return RedirectToAction("Users");
        }
      }

      //post/ admin/deleteUser/userId
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            var currentUserId= int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _userService.DeleteUserAsync( id,currentUserId);
            TempData["SuccessMessage"]= "User deleted successfully.";
            return RedirectToAction("Users");
        }
        catch(Exception ex)
        {
            TempData["ErrorMessage"]= "An error occurred while deleting user: " + ex.Message;
            return RedirectToAction("Users");
        }
    }

        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVm registerVm)
        {
           try{
        if (!ModelState.IsValid)
            {
                return View(registerVm);
            }

            var user= await _authService.UsernameExistsAsync(registerVm.Username);
            if(user)
            {
                ModelState.AddModelError("Username","Username already exists");
                return View(registerVm);
            }

            var email= await _authService.EmailExistsAsync(registerVm.Email);
            if(email)
            {
                ModelState.AddModelError("Email","Email already exists");
                return View(registerVm);
            }

            await _authService.RegisterUserAsync(registerVm.Username,registerVm.Email,registerVm.Password);
            TempData["SuccessMessage"]="Registration successful. Please log in.";
            return RedirectToAction("Users");
           }
           catch(Exception ex)
           {
            TempData["ErrorMessage"]= "Registration failed " + ex.Message;
            return View(registerVm);
           }
    }

    }

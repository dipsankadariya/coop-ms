using System.Security.Claims;
using bms.Data.DTOs;
using bms.Services.Interfaces;
using bms.ViewModels.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bms.Controllers
{ 
public class AccountController:Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVm loginVm)
        {
            try{
                
            if (!ModelState.IsValid)
            {
                return View(loginVm);
            }

            UserDto? user = null;
            try
            {
                user = await _authService.ValidateUserAsync(loginVm.Username, loginVm.Password);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(loginVm);
            }

            // Set up authentication cookie

            var claims= new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,user.Role),
            };

            var claimsIdentity= new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity));

            TempData["SuccessMessage"]=$"Welcome back, {user.Username}!";
            return RedirectToAction("Index", "Member");
            
        }
        catch(Exception ex){
            TempData["ErrorMessage"]= "Login failed " + ex.Message;
            return View(loginVm);
            }   
        }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
       try{
         await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        TempData["SuccessMessage"]="You have been logged out successfully.";
        return RedirectToAction("Login");
       }
       catch(Exception ex){
        TempData["ErrorMessage"]= "Logout failed " + ex.Message;
        return RedirectToAction("Index","Member");
       }
    }
}
}


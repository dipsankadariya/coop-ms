using System.Security.Claims;
using bms.Services.Interfaces;
using bms.ViewModels.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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

          var user= await _authService.ValidateUserAsync(loginVm.Username,loginVm.Password);

            if (user == null)
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
            return RedirectToAction("Login");
           }
           catch(Exception ex)
           {
            TempData["ErrorMessage"]= "Registration failed " + ex.Message;
            return View(registerVm);
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


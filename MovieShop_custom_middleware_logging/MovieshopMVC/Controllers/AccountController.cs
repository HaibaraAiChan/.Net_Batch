using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Models;
using ApplicationCore.Contracts.Services;
using Infrastructure.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace MovieshopMVC.Controllers

{
    public class AccountController : Controller
    {
        public readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            _logger.LogInformation("Rendering Login View"); // write Log information
            _logger.LogError("This is a sample error log for Login View"); // write Log error
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login( LoginModel model)
        {
            var user = await _accountService.ValidateUser(model.Email, model.Password);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("DateOfBirth", user.DateOfBirth.ToString("yyyy-MM-dd"))
            };
            var claimsidentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsidentity));

            return LocalRedirect("~/");
        }
        [HttpGet]//[Route("Register")] for register link
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]//    [Route("Register")] for submit button
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var usr= await _accountService.RegisterUser(model);
            return RedirectToAction("Login");

        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return LocalRedirect("~/");
        }
    }
}

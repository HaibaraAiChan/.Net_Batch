using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Models;
using ApplicationCore.Contracts.Services;
using Infrastructure.Services;

namespace MovieshopMVC.Controllers

{
    public class AccountController : Controller
    {
        public readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login( LoginModel model)
        {
            return View();
        }
        [HttpGet]//[Route("Register")] for register link
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]//    [Route("Register")] for submit button
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var usr= await _accountService.RegisterUser(model);
            return RedirectToAction("Login");

        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}

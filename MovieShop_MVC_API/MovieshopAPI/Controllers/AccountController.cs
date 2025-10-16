using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Models;

namespace MovieshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }
        
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login( LoginModel model)
        {
            
            var user = await _accountService.ValidateUser(model.Email, model.Password);
            //if (user == null)
            //{
            //    return Unauthorized(new { errorMessage = "Invalid email or password" });
            //}
            return Ok(user);
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register( RegisterModel model)
        {
            
            var user = await _accountService.RegisterUser(model);
            //if (user == null)
            //{
            //    return BadRequest(new { errorMessage = "Registration failed" });
            //}
            return Ok(user);
        }
    }
}

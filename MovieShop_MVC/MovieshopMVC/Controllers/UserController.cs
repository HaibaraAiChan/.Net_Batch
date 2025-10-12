using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieshopMVC.Controllers
{
    [Authorize] // Ensure the user is authenticated for all actions in this controller
    public class UserController : Controller
    {
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> Purchases()
        {
            //var isLoggedIn = this.HttpContext.User.Identity.IsAuthenticated;
            //if (isLoggedIn) {
            //    var userEmail = this.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            //    var UserId = this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //}else{
            //return RedirectToAction("Login", "Account");

            
            var userId = GetUserId();
            return View();
        }
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> Favorites()
        {
            var userId = GetUserId();
            return View();
        }
        public IActionResult Reviews()
        {
            var userId = GetUserId();
            return View();
        }
        private int GetUserId()
        {
            var userId = Convert.ToInt32(this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return userId;
        }
    }
}

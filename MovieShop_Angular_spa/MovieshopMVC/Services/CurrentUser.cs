using Microsoft.AspNetCore.Http;
using System.Security.Claims;
namespace MovieshopMVC.Services
{
    public class CurrentUser: ICurrentUser
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CurrentUser(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public int? UserId =>  Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        public string? Email =>  _contextAccessor.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value ;
        public bool IsAuthenticated => _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
        public string? FullName =>  _contextAccessor.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.GivenName)?.Value
                 + " "+ _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
        public IEnumerable<string> Roles => _contextAccessor.HttpContext.User.FindAll(c => c.Type == "role").Select(c => c.Value) ;
        public bool isAdmin => Roles.Contains("Admin");

    }
}

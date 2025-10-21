using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using ApplicationCore.Contracts.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Collections.Generic;

namespace MovieshopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;
        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
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
            //jwt
            var claims= new List<Claim> //
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName), //given_name
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName), //family_name
                new Claim(JwtRegisteredClaimNames.Email, user.Email), //email
                new Claim("language", "English"),

            };
            var claimIdentity = new ClaimsIdentity(claims); // create a ClaimsIdentity object
            var privateKey = _configuration["PrivateKey"];
            var expirationTime = _configuration.GetValue<int>("expirationHours");
            var issuer = _configuration["Issuer"];
            var audience = _configuration["Audience"];

            var secreteKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(privateKey));
            var credentials = new SigningCredentials(secreteKey, SecurityAlgorithms.HmacSha256);

            var tokenDescription = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Subject = claimIdentity,
                Issuer = issuer,
                Audience = audience,
                Expires = DateTime.UtcNow.AddHours(expirationTime),

            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.CreateToken(tokenDescription);
            return Ok(new
            {
                token = tokenHandler.WriteToken(jwtToken)
            });

            //return Ok(user);
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

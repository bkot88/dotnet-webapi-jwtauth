
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TokenDemo.Data;
using TokenDemo.Models;

namespace TokenDemo.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration Configuration)
        {
            _userManager = userManager;
            _configuration = Configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromHeader] BasicAuthModel model)
        {
            if (!ModelState.IsValid || !model.IsValid())
            {
                return BadRequest("Basic Auth required in request header!");
            }

            model.Parse();
            string username = model.Username;
            string password = model.Password;

            var user = await _userManager.FindByNameAsync(username);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JwtSecretKey")));
                SigningCredentials credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);
                Claim[] claimsData = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName)
                };

                var token = new JwtSecurityToken(
                    issuer:"example.capital",
                    audience: "example.capital",
                    claims: claimsData,
                    signingCredentials: credentials,
                    expires: DateTime.UtcNow.AddHours(1));

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            return Unauthorized();
        }
    }
}
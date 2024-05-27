using Autofac;
using Infrastructure.Entities;
using Infrastructure.Exceptions;
using Infrastructure.Services;
using MainServiceAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MainServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors()]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly ILifetimeScope _scope;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, ILifetimeScope scope, ILogger<AuthController> logger,IUserService userService)
        {
            _configuration = configuration;
            _scope = scope;
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            model.ResolveDependency(_scope);
            var user = model.Login(model.Email, model.Password);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                Expires = DateTime.UtcNow.AddDays(7),
                Subject = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            Response.Cookies.Append("token", tokenString, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });

            return Ok(new { token = tokenString });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            try
            {
                model.ResolveDependency(_scope);
                model.AddUser();
                return Ok("Registration Complete");
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message, e);
                return BadRequest(e.Message);
            }
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("test")]
        public IActionResult Test()
        {

            // Get current user ID
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Get current user from database
            var user = _userService.GetUserById(Guid.Parse(userId));

            // Check if user has "Admin" role
            if (user != null && user.Role == "Admin")
            {
                return Ok("Authorized.");
            }
            else
            {
                return Forbid();
            }
        }
    }
}

using Autofac;
using Infrastructure.Exceptions;
using MainServiceAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
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
    [Authorize(Roles ="Admin,Doctor")]
    public class DoctorController : ControllerBase
    {
        private readonly ILifetimeScope _scope;
        private readonly IConfiguration _configuration;
        private readonly ILogger<DoctorController> _logger;

        public DoctorController(ILifetimeScope scope,IConfiguration configuration,ILogger<DoctorController> logger)
        {
            _scope = scope;
            _configuration = configuration;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public IActionResult Register(DoctorModel model)
        {
            try
            {
                model.ResolveDependency(_scope);
                model.Create();
                return Ok();
            }
            catch (DuplicateException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { msg = "Registration Failed" });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult Login(DoctorModel model)
        {
            model.ResolveDependency(_scope);
            try
            {
                var doctor=model.Login();
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"],
                    Expires = DateTime.UtcNow.AddDays(7),
                    Subject = new ClaimsIdentity(new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, doctor.Id.ToString()),
                        new Claim(ClaimTypes.Email, doctor.Email),
                        new Claim(ClaimTypes.Role, doctor.Role)
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
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public IActionResult Add(DoctorModel model)
        {
            try
            {
                model.ResolveDependency(_scope);
                model.Create();
                return Ok(new { msg = "Doctor Created" });
            }
            catch(DuplicateException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,new {msg="Cannot Create Doctor"});
            }
        }

        [HttpPut]
        public IActionResult Put(DoctorModel model)
        {
            model.ResolveDependency(_scope);
            try
            {
                model.Update();
                return Ok(new { msg = "Doctor Profile Updated" });
            }
            catch (DuplicateException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { msg = "Cannot Create Doctor" });
            }
        }

        [HttpDelete]
        public IActionResult Delete(DoctorModel model) 
        {
            try
            {
                model.ResolveDependency(_scope);
                model.Delete();
                return Ok(new { msg = "Deleted" });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new { msg = "Cannot Delete" });
            }
        }

        [HttpGet]
        public IActionResult Get() 
        { 
            var model = _scope.Resolve<DoctorModel>();
            var data=model.GetDoctors();
            return Ok(new { result = data });
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var model = _scope.Resolve<DoctorModel>();
            var data=model.GetDoctor(id);
            return Ok(new { result = data });
        }
    }
}

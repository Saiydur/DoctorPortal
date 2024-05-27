using Autofac;
using Infrastructure.BusinessObjects;
using Infrastructure.RabbitMQ;
using Infrastructure.Services;
using MainServiceAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MainServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors()]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<UserController> _logger;
        private readonly IRabitMQProducer _rabbitmqProducer;
        private readonly IUserService _userService;

        public UserController(ILifetimeScope scope, ILogger<UserController> logger, IRabitMQProducer rabitMQProducer, IUserService userService)
        {
            _scope = scope;
            _logger = logger;
            _rabbitmqProducer = rabitMQProducer;
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("users")]
        public IEnumerable<User> GetUsers()
        {
            try
            {
                var model = _scope.Resolve<UserModel>();
                return model.GetUsers();
            }
            catch (Exception ex)
            {
                _logger.LogError("User Not Found", ex.Message);
                return null;
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("users/{id}")]
        public IActionResult GetUser(Guid id)
        {
            //_rabbitmqProducer.SendMessage("user","get-user",id);
            //var result = _rabbitmqProducer.RecivedMessage("user-response");
            try
            {
                var user = _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        [Route("profile")]
        public IActionResult Profile()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = _userService.GetUserById(Guid.Parse(userId));

            return Ok(user);
        }

        [HttpPut]
        [Authorize(Roles = "Admin,User")]
        [Route("profile")]
        public IActionResult UpdateProfile(UserModel user)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            try
            {
                user.Id = Guid.Parse(userId);
                user.ResolveDependency(_scope);
                user.Edit(Guid.Parse(userId));
                return Ok(new { value = user, msg = "User Updated" });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest("Cannot Update");
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _userService.DeleteUser(id);
                return Ok("Deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Add")]
        public IActionResult Add(UserModel user)
        {
            try
            {
                user.ResolveDependency(_scope);
                user.Add();
                return Ok("User Created");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }
    }
}

using Autofac;
using Infrastructure.Exceptions;
using MainServiceAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MainServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors()]
    [Authorize(Roles = "Admin")]
    public class SpecialityController : ControllerBase
    {
        public readonly ILifetimeScope _scope;
        public readonly ILogger<SpecialityController> _logger;

        public SpecialityController(ILifetimeScope scope, ILogger<SpecialityController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post(SpecialityModel model)
        {
            try
            {
                model.ResolveDependency(_scope);
                model.Add();
                return Ok();
            }
            catch (DuplicateException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new { msg = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Cannot Add Speciality");
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            var model = _scope.Resolve<SpecialityModel>();
            try
            {
                var result = model.GetSpecialities();
                return Ok(new { data = result });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { msg = "Cannot Get Speciality" });
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var model = _scope.Resolve<SpecialityModel>();
            try
            {
                var result = model.GetSpeciality(id);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { msg = "Cannot Get Speciality" });
            }
        }

        [HttpPut]
        public IActionResult Put(SpecialityModel model)
        {
            model.ResolveDependency(_scope);
            try
            {
                model.Update(model);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { msg = "Cannot Update Speciality" });
            }
        }

        [HttpDelete]
        public IActionResult Delete(SpecialityModel model)
        {
            model.ResolveDependency(_scope);
            try
            {
                model.Remove(model);
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { msg = "Cannot Delete Speciality" });
            }
        }
    }
}

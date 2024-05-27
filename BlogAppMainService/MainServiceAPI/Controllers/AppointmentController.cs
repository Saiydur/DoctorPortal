using Autofac;
using Infrastructure.Exceptions;
using MainServiceAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MainServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors()]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(ILifetimeScope scope, ILogger<AppointmentController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post(AppointmentModel appointmentModel)
        {
            try
            {
                appointmentModel.ResolveDependency(_scope);
                appointmentModel.Add();
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Cannot Add Appointment");
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            var model = _scope.Resolve<AppointmentModel>();
            var result = model.GetAppointments();
            return Ok(new { data = result });
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var model = _scope.Resolve<AppointmentModel>();
            try
            {
                var result = model.GetAppointmentById(id);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}", ex);
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(AppointmentModel model)
        {
            model.ResolveDependency(_scope);
            model.Update();
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(AppointmentModel model)
        {
            model.ResolveDependency(_scope);
            try
            {
                model.Remove();
                return Ok(new { data = "Appointment Deleted" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}", ex);
                return NotFound(new { data = "Appointment Cannot Delete" });
            }
        }

        //[HttpGet("doctor/{id}")]
        //public IActionResult GetByDoctorId(Guid id)
        //{
        //    var model = _scope.Resolve<AppointmentModel>();
        //    try
        //    {
        //        var result = model.GetAppointmentByDoctorId(id);
        //        return Ok(new { data = result });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"{ex.Message}", ex);
        //        return NotFound(ex.Message);
        //    }
        //}
    }
}

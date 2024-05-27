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
    [Authorize(Roles ="Admin")]
    public class ConsultationController : ControllerBase
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<ConsultationController> _logger;

        public ConsultationController(ILifetimeScope scope, ILogger<ConsultationController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post(ConsultationModel consultationModel)
        {
            try
            {
                consultationModel.ResolveDependency(_scope);
                consultationModel.Add();
                return Ok();
            }
            catch(DuplicateException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new {msg= ex.Message});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Cannot Add Consultation");
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            var model = _scope.Resolve<ConsultationModel>();
            var result = model.GetConsultations();
            return Ok(new {data=result});
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var model = _scope.Resolve<ConsultationModel>();
            try
            {
                var result = model.GetConsultationById(id);
                return Ok(new { data = result });
            }
            catch(Exception ex)
            {
                _logger.LogError($"{ex.Message}", ex);
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(ConsultationModel model)
        {
            model.ResolveDependency(_scope);
            model.UpdateConsultation();
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(ConsultationModel model)
        {
            model.ResolveDependency(_scope);
            try
            {
                model.DeleteConsultation();
                return Ok(new {data= "Consultation Deleted" });
            }
            catch(Exception ex)
            {
                _logger.LogError($"{ex.Message}", ex);
                return NotFound(new { data = "Consultation Cannot Delete" });
            }
        }
    }
}

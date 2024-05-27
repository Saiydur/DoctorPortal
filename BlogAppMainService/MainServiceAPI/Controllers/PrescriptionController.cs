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
    public class PrescriptionController : ControllerBase
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<PrescriptionController> _logger;

        public PrescriptionController(ILifetimeScope scope, ILogger<PrescriptionController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post(PrescriptionModel prescriptionModel)
        {
            try
            {
                prescriptionModel.ResolveDependency(_scope);
                prescriptionModel.Add();
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Cannot Add Prescription");
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            var model = _scope.Resolve<PrescriptionModel>();
            var result = model.GetPrescriptions();
            return Ok(new { data = result });
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var model = _scope.Resolve<PrescriptionModel>();
            try
            {
                var result = model.GetPrescriptionById(id);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Cannot Get Prescription");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(PrescriptionModel prescriptionModel)
        {
            try
            {
                prescriptionModel.ResolveDependency(_scope);
                prescriptionModel.Update();
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Cannot Update Prescription");
            }
        }

        [HttpDelete]
        public IActionResult Delete(PrescriptionModel prescriptionModel)
        {
            var model = _scope.Resolve<PrescriptionModel>();
            try
            {
                model.Remove();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Cannot Delete Prescription");
            }
        }
    }
}

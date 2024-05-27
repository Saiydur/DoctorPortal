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
    public class PaymentController : ControllerBase
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(ILifetimeScope scope, ILogger<PaymentController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post(PaymentModel paymentModel)
        {
            try
            {
                paymentModel.ResolveDependency(_scope);
                paymentModel.Add();
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Cannot Add Payment");
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            var model = _scope.Resolve<PaymentModel>();
            var result = model.GetPayments();
            return Ok(new { data = result });
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var model = _scope.Resolve<PaymentModel>();
            try
            {
                var result = model.GetPaymentById(id);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}", ex);
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(PaymentModel model)
        {
            model.ResolveDependency(_scope);
            model.Update();
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(PaymentModel model)
        {
            model.ResolveDependency(_scope);
            try
            {
                model.Remove();
                return Ok(new { data = "Payment Deleted" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}", ex);
                return NotFound(new { data = "Payment Cannot Delete" });
            }
        }
    }
}

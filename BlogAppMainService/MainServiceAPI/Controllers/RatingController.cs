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
    public class RatingController : ControllerBase
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<RatingController> _logger;

        public RatingController(ILifetimeScope scope, ILogger<RatingController> logger)
        {
            _scope = scope;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post(RatingModel ratingModel)
        {
            try
            {
                ratingModel.ResolveDependency(_scope);
                ratingModel.Create();
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Cannot Add Rating");
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            var model = _scope.Resolve<RatingModel>();
            var result = model.GetRatings();
            return Ok(new { data = result });
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var model = _scope.Resolve<RatingModel>();
            try
            {
                var result = model.GetRating(id);
                return Ok(new { data = result });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}", ex);
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(RatingModel model)
        {
            model.ResolveDependency(_scope);
            model.Update();
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(RatingModel model)
        {
            model.ResolveDependency(_scope);
            try
            {
                model.Delete();
                return Ok(new { data = "Rating Deleted" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}", ex);
                return NotFound(new { data = "Rating Cannot Delete" });
            }
        }
    }
}

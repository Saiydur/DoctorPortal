using Autofac;
using Infrastructure.Exceptions;
using Infrastructure.Services;
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
    public class CategoryController : ControllerBase
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger, ILifetimeScope scope)
        {
            _scope = scope;
            _logger = logger;
        }

        [HttpPost()]
        public IActionResult Post(CategoryCreateModel model)
        {
            try
            {
                model.ResolveDependency(_scope);
                model.Add();
                return Ok(new { data = "Category Created" });
            }
            catch (DuplicateException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Cannot Create");
            }
        }

        [HttpGet()]
        public IActionResult Get()
        {
            try
            {
                var model = _scope.Resolve<CategoryCreateModel>();
                var categoryList = model.Get();
                return Ok(new { data = categoryList });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Cannot Get");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var model = _scope.Resolve<CategoryCreateModel>();
                var category = model.Get(id);
                return Ok(new { data = category });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Cannot Get");
            }
        }

        [HttpPatch]
        public IActionResult Put(CategoryUpdateModel model)
        {
            try
            {
                model.ResolveDependency(_scope);
                model.Update();
                return Ok(new { data = "Category Updated" });
            }
            catch (DuplicateException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Cannot Update");
            }
        }

        [HttpDelete()]
        public IActionResult Delete(CategoryUpdateModel model)
        {
            try
            {
                model.ResolveDependency(_scope);
                model.Delete();
                return Ok(new { data = "Category Deleted" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Cannot Delete");
            }
        }
    }
}

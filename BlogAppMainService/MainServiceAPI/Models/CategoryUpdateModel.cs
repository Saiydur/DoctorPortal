using Autofac;
using AutoMapper;
using Infrastructure.BusinessObjects;
using Infrastructure.Services;

namespace MainServiceAPI.Models
{
    public class CategoryUpdateModel
    {
        private ICategoryService _categoryService;
        private IMapper _mapper;

        public Guid Id { get; set; }
        public string Name { get; set; }

        public CategoryUpdateModel()
        {

        }

        public CategoryUpdateModel(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _categoryService = scope.Resolve<ICategoryService>();
            _mapper = scope.Resolve<IMapper>();
        }

        internal void Update()
        {
            var categoryBO = _mapper.Map<Category>(this);
            _categoryService.UpdateCategory(categoryBO);
        }

        internal void Delete()
        {
            var categoryBO = _mapper.Map<Category>(this);
            _categoryService.DeleteCategory(categoryBO);
        }
    }
}

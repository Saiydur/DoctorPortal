using Autofac;
using AutoMapper;
using Infrastructure.BusinessObjects;
using Infrastructure.Services;

namespace MainServiceAPI.Models
{
    public class CategoryCreateModel
    {
        private ICategoryService _categoryService;
        private IMapper _mapper;

        public string Name { get; set; }

        public CategoryCreateModel()
        {
            
        }

        public CategoryCreateModel(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _categoryService = scope.Resolve<ICategoryService>();
            _mapper = scope.Resolve<IMapper>();
        }

        internal void Add()
        {
            var categoryBO = _mapper.Map<Category>(this);
            _categoryService.AddCategory(categoryBO);
        }

        internal IList<CategoryCreateModel> Get()
        {
            var categoryBOs = _categoryService.GetCategories();
            var categoryCreateModels = new List<CategoryCreateModel>();
            foreach (var categoryBO in categoryBOs)
            {
                var categoryCreateModel = _mapper.Map<CategoryCreateModel>(categoryBO);
                categoryCreateModels.Add(categoryCreateModel);
            }
            return categoryCreateModels;
        }

        internal CategoryCreateModel Get(Guid id)
        {
            var categoryBO = _categoryService.GetCategory(id);
            var categoryCreateModel = _mapper.Map<CategoryCreateModel>(categoryBO);
            return categoryCreateModel;
        }
    }
}

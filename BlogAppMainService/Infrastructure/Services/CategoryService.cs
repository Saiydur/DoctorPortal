using AutoMapper;
using Infrastructure.Exceptions;
using Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CategoryBO = Infrastructure.BusinessObjects.Category;
using CategoryEO = Infrastructure.Entities.Category;

namespace Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        public void AddCategory(CategoryBO category)
        {
            var categoryNameCount = _applicationUnitOfWork.Categories.GetCount(x=> x.Name == category.Name);
            if (categoryNameCount > 0)
                throw new DuplicateException("Category Name Already Exist");
            var categoreyEO = _mapper.Map<CategoryEO>(category);
            _applicationUnitOfWork.Categories.Add(categoreyEO);
            _applicationUnitOfWork.Save();
        }

        public IList<CategoryBO> GetCategories()
        {
            var categoryEO = _applicationUnitOfWork.Categories.GetAll();
            IList<CategoryBO> categories = new List<CategoryBO>();
            foreach (var category in categoryEO)
            {
                categories.Add(_mapper.Map<CategoryBO>(category));
            }
            return categories;
        }

        public void UpdateCategory(CategoryBO category)
        {
            var categoryEO = _applicationUnitOfWork.Categories.GetById(category.Id);
            if (categoryEO == null)
                throw new InvalidOperationException("Category Not Found");
            categoryEO = _mapper.Map(category,categoryEO);
            _applicationUnitOfWork.Save();
        }

        public void DeleteCategory(CategoryBO categoryBO)
        {
            _applicationUnitOfWork.Categories.Remove(categoryBO.Id);
            _applicationUnitOfWork.Save();
        }

        public CategoryBO GetCategory(Guid id)
        {
            var categoryEO = _applicationUnitOfWork.Categories.GetById(id);
            var categoryBO = _mapper.Map<CategoryBO>(categoryEO);
            return categoryBO;
        }
    }
}

using Infrastructure.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface ICategoryService
    {
        public void AddCategory(Category category);

        public IList<Category> GetCategories();

        public Category GetCategory(Guid id);

        public void UpdateCategory(Category category);

        public void DeleteCategory(Category category);
    }
}

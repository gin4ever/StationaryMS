using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Models;
namespace eProject.Services
{
    public interface ICategoryServices
    {
        List<Category> GetCategories();
        Category AddCategory(Category category);
        bool UpdateCategory(Category category);
        Category GetCategory(int id);
        bool DeleteCategory(int id);
    }
}

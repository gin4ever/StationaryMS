using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.Models;
using eProject.Repository;
namespace eProject.Services
{
    public class CategoryServices : ICategoryServices
    {
        private StationeryContext context;
        public CategoryServices(StationeryContext context)
        {
            this.context = context;
        }

        public Category AddCategory(Category newCategory)
        {
            var model = context.Category.SingleOrDefault(m => m.Category_Id.Equals(newCategory.Category_Id));
            if (model == null)
            {
                context.Category.Add(newCategory);
                context.SaveChanges();
                return newCategory;
            }
            else
            {
                return null;
            }
        }

        public List<Category> GetCategories()
        {
            return context.Category.ToList();
        }

        public Category GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}

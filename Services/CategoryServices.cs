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

        public bool DeleteCategory(int id)
        {
            var model = context.Category.SingleOrDefault(m => m.Category_Id.Equals(id));
            if (model != null)
            {
                context.Category.Remove(model);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Category> GetCategories()
        {
            return context.Category.ToList();
        }

        public Category GetCategory(int id)
        {
            var model = context.Category.SingleOrDefault(m => m.Category_Id.Equals(id));
            if (model != null)
            {
                return model;
            }
            else
            {
                return null;
            }
        }

        public bool UpdateCategory(Category editCategory)
        {
            var model = context.Category.SingleOrDefault(m => m.Category_Id.Equals(editCategory.Category_Id));
            if (model != null)
            {
                model.Description = editCategory.Description;
                model.Status = editCategory.Status;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

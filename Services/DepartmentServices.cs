using eProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Repository;
namespace eProject.Services
{
    public class DepartmentServices : IDepartmentServices
    {
        private StationeryContext context;
        public DepartmentServices(StationeryContext context)
        {
            this.context = context;
        }

        public Department AddDepartment(Department newDepartment)
        {
            var model = context.Department.SingleOrDefault(m => m.Department_Id.Equals(newDepartment.Department_Id));
            if (model == null)
            {
                context.Department.Add(newDepartment);
                context.SaveChanges();
                return newDepartment;
            }
            else
            {
                return null;
            }
        }

        public bool DeleteDepartment(int id)
        {
            var model = context.Department.SingleOrDefault(m => m.Department_Id.Equals(id));
            if (model != null)
            {
                context.Department.Remove(model);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Department GetDepartment(int id)
        {
            var model = context.Department.SingleOrDefault(m => m.Department_Id.Equals(id));
            if (model != null)
            {
                return model;
            }
            else
            {
                return null;
            }
        }

        public List<Department> GetDepartments()
        {
            return context.Department.ToList();
        }

        public bool UpdateDepartment(Department editDepartment)
        {
            var model = context.Department.SingleOrDefault(m => m.Department_Id.Equals(editDepartment.Department_Id));
            if (model != null)
            {
                model.DepartmentName = editDepartment.DepartmentName;
                model.Budget = editDepartment.Budget;
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

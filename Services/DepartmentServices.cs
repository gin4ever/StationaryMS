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

        public Department GetDepartment(int id)
        {
            return context.Department.SingleOrDefault(a => a.Department_Id == id);
        }

        public List<Department> GetDepartments()
        {
            return context.Department.ToList();
        }
    }
}

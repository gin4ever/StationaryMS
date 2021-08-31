using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Models;
namespace eProject.Services
{
    public interface IDepartmentServices
    {
        List<Department> GetDepartments();
        Department AddDepartment(Department newDepartment);
        bool UpdateDepartment(Department editDepartment);
        Department GetDepartment(int id);
        bool DeleteDepartment(int id);
    }
}

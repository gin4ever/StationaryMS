using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Models;
namespace eProject.Services
{
    public interface IRoleServices
    {
        List<Role> GetRoles();
        int CountRole(Department department);
        Role GetRole(int id);
        Role highestRole();


        bool AddRole(Role newRole);
        bool UpdateRole(Role editRole);
        bool DeleteRole(int id);
    }
}

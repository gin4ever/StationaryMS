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
        bool AddRole(Role newRole);
        bool UpdateRole(Role editRole);
        Role GetRole(int id);
        bool DeleteRole(int id);
    }
}

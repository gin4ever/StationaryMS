using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.Models;
using eProject.Repository;
namespace eProject.Services
{
    public class RoleServices : IRoleServices
    {
        private StationeryContext context;
        public RoleServices(StationeryContext context)
        {
            this.context = context;
        }
        public List<Role> GetRoles()
        {
            return context.Role.ToList();
        }
        public int CountRole(Department department)
        {
            return context.Role.Where(i => i.Role_Id.Equals(department.Department_Id)).ToList().Count;

        }

        public Role GetRole(int id)
        {
            return context.Role.SingleOrDefault(a => a.Role_Id == id);
        }

        public Role highestRole()
        {
            var role = context.Role.ToList();
            int count = role.Count();
            var highestroleId = context.Role.SingleOrDefault(a=>a.Role_Id.Equals(role[count].Role_Id));
            return highestroleId;
        }
    }
}

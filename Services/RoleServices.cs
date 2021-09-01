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

        public bool UpdateRole(Role editRole)
        {
            var model = context.Role.SingleOrDefault(m => m.Role_Id.Equals(editRole.Role_Id));
            if (model != null)
            {
                model.RoleName = editRole.RoleName;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddRole(Role newRole)
        {
            context.Role.Add(newRole);
            context.SaveChanges();
            return true;
        }

        public bool DeleteRole(int id)
        {
            var model = context.Role.SingleOrDefault(m => m.Role_Id.Equals(id));
            if (model != null)
            {
                context.Role.Remove(model);
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

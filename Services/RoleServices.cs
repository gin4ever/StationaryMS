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

        public Role GetRole(int id)
        {
            var model = context.Role.SingleOrDefault(m => m.Role_Id.Equals(id));
            if (model != null)
            {
                return model;
            }
            else
            {
                return null;
            }
        }

        public List<Role> GetRoles()
        {
            return context.Role.ToList();
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
    }
}

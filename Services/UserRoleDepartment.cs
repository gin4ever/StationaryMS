using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.Models;
using eProject.Repository;
namespace eProject.Services
{
    public class UserRoleDepartment : IUserRoleDepartment
    {
        private StationeryContext context;
        public UserRoleDepartment(StationeryContext context)
        {
            this.context = context;
        }
        public vUserRoleDepartment GetUserRoleDepartments(int id)
        {
            var model = context.vUserRoleDepartment.SingleOrDefault(m => m.User_Id.Equals(id));
            if (model != null)
            {
                return model;
            }
            else
            {
                return null;
            }
        }
    }
}

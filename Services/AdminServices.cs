using eProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Repository;
namespace eProject.Services
{
    public class AdminServices : IAdminServices
    {
        private StationeryContext context;
        public AdminServices(StationeryContext context)
        {
            this.context = context;
        }
        public Admins checkLogin(Admins admin)
        {
            var model = context.Admins.SingleOrDefault(m => m.Username.Equals(admin.Username));
            if (model != null)
            {
                string pass = PinCodeSecurity.pinDecrypt(model.Password);
                if (admin.Password.Equals(pass))
                {
                    return model;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public Admins createAdmin(Admins newAdmin)
        {
            var model = context.Admins.SingleOrDefault(m => m.Admin_Id.Equals(newAdmin.Admin_Id));
            if (model == null)
            {
                newAdmin.Password = PinCodeSecurity.pinEncrypt(newAdmin.Password);
                context.Admins.Add(newAdmin);
                context.SaveChanges();
                return newAdmin;
            }
            else
            {
                return null;
            }
        }

        public Admins GetAdmin(int id)
        {
            var model = context.Admins.SingleOrDefault(m => m.Admin_Id.Equals(id));
            if (model != null)
            {
                return model;
            }
            else
            {
                return null;
            }
        }

        public List<Admins> GetAdmins()
        {
            return context.Admins.ToList();
        }
    }
}

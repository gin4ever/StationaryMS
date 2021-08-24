using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Models;
namespace eProject.Services
{
    public interface IAdminServices
    {
        List<Admins> GetAdmins();
        Admins checkLogin(Admins admin);
        Admins GetAdmin(string uname);
        Admins createAdmin(Admins newAdmin);
        bool updateAdmin(Admins editAdmin);
    }
}

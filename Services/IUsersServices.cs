using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Models;
namespace eProject.Services
{
    public interface IUsersServices
    {
        List<Users> GetUsers();
        Users checkLogin(Users user);
        Users createUser(Users newUser);
        Users GetUser(string uname);
        Users GetUserByRoleID(int RoleId, int deptcode);
        bool UpdateProfile(Users editUser);

        int CountRole(Users department);
    }
}

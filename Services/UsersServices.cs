using eProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Repository;
namespace eProject.Services
{
    public class UsersServices : IUsersServices
    {
        private StationeryContext context;
        public UsersServices(StationeryContext context)
        {
            this.context = context;
        }
        public Users checkLogin(Users user)
        {
            var model = context.Users.SingleOrDefault(u => u.Username.Equals(user.Username));
            if (model != null)
            {
                string pass = PinCodeSecurity.pinDecrypt(model.Password);
                if (user.Password.Equals(pass))
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

        public Users createUser(Users newUser)
        {
            var model = context.Users.SingleOrDefault(m => m.User_Id.Equals(newUser.User_Id));
            if (model == null)
            {
                newUser.Password = PinCodeSecurity.pinEncrypt(newUser.Password);
                context.Users.Add(newUser);
                context.SaveChanges();
                return newUser;
            }
            else
            {
                return null;
            }
        }

        public Users GetUser(string uname)
        {
            var model = context.Users.SingleOrDefault(u => u.Username.Equals(uname));
            if (model != null)
            {
                return model;
            }
            else
            {
                return null;
            }

        }

        public Users GetUserByRoleID(int RoleID, int deptcode)
        {
            var model = context.Users.FirstOrDefault(u => u.Role_Id.Equals(RoleID) && u.Department_Id.Equals(deptcode));
            if (model != null)
            {
                return model;
            }
            else
            {
                return null;
            }

        }

        public List<Users> GetUsers()
        {
            return context.Users.ToList();
        }

        public bool UpdateProfile(Users editUser)
        {
            var model = context.Users.SingleOrDefault(m => m.Username.Equals(editUser.Username));
            if (model != null)
            {
                model.Password = PinCodeSecurity.pinEncrypt(editUser.NewPassword);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Users GetUser(int userId)
        {
            var model = context.Users.SingleOrDefault(u => u.User_Id.Equals(userId));
            if (model != null)
            {
                return model;
            }
            else
            {
                return null;
            }

        }

        public int CountRole(Users department)
        {
            return context.Users.Where(i => i.Department_Id.Equals(department.Department_Id)).ToList().Count;
        }

        public Users GetUserByRole(int RoleId)
        {
            var model = context.Users.FirstOrDefault(u => u.Role_Id.Equals(RoleId));
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

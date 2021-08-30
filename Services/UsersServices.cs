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

        public List<Users> GetUsers()
        {
            return context.Users.ToList();
        }

        public bool UpdateProfile(Users editUser)
        {
            var model = context.Users.SingleOrDefault(m => m.User_Id.Equals(editUser.User_Id));
            if (model != null)
            {
                model.Password = PinCodeSecurity.pinEncrypt(editUser.Password);
                model.Images = editUser.Images;
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

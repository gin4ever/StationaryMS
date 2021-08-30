using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Models;
using eProject.Services;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;

namespace eProject.Controllers
{
    public class UserController : Controller
    {
        private IUsersServices services;
        private IUserRoleDepartment uservices;
        public UserController(IUsersServices services, IUserRoleDepartment uservices)
        {
            this.services = services;
            this.uservices = uservices;
        }
        public IActionResult Index(string name)
        {

            string json_user_session = HttpContext.Session.GetString("user_session");
            JObject jsonResponseUser = null;
            Users user = null;
            if (json_user_session != null)
            {
                //lấy session User
                jsonResponseUser = JObject.Parse(json_user_session);
                user = JsonConvert.DeserializeObject<Users>(jsonResponseUser.ToString());

                if (user != null)
                {
                    ViewBag.session = HttpContext.Session.GetString("username");
                    var listAdmin = services.GetUsers();
                    if (string.IsNullOrEmpty(name))
                    {
                        return View(listAdmin);
                    }
                    else
                    {
                        listAdmin = services.GetUsers().Where(a => a.Username.ToLower().Contains(name.ToLower())).ToList();
                        return View(listAdmin);
                    }
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }
            }
            return View();
        }

        public IActionResult AdminIndexUser(string uname)
        {
            string json_admin_session = HttpContext.Session.GetString("admin_session");
            JObject jsonResponseAdmin = null;
            Admins admin = null;
            if (json_admin_session != null)
            {
                //lấy session Admin
                jsonResponseAdmin = JObject.Parse(json_admin_session);
                admin = JsonConvert.DeserializeObject<Admins>(jsonResponseAdmin.ToString());

                if (admin != null)
                {
                    ViewBag.session = HttpContext.Session.GetString("username");
                    var listUsers = services.GetUsers();
                    if (string.IsNullOrEmpty(uname))
                    {
                        return View(listUsers);
                    }
                    else
                    {
                        listUsers = services.GetUsers().Where(u => u.Fullname.ToLower().Contains(uname.ToLower())).ToList();
                        return View(listUsers);
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Admin");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Users user)
        {
            try
            {
                var model = new Users
                {
                    Username = user.Username,
                    Password = user.Password
                };

                Users acc = services.checkLogin(model);
                if (acc != null)
                {
                    HttpContext.Session.SetInt32("id", user.User_Id);
                    HttpContext.Session.SetString("username", user.Username);
                    HttpContext.Session.SetString("password", user.Password);
                    HttpContext.Session.SetString("user_session", JsonConvert.SerializeObject(acc));
                    return RedirectToAction("Index", "Request");
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }

            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Login");
            }
        }

        //log out
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        //[HttpGet]
        //public IActionResult Profile(Users user)
        //{
        //    var model = services.GetUser(user.Username);
        //    return View(model);
        //}

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //hien thi profile
        [HttpGet]
        public IActionResult Profile(string uname)
        {
            string json_user_session = HttpContext.Session.GetString("user_session");
            JObject jsonResponseUser = null;
            Users user = null;
            if (json_user_session != null)
            {
                //lấy session User
                jsonResponseUser = JObject.Parse(json_user_session);
                user = JsonConvert.DeserializeObject<Users>(jsonResponseUser.ToString());
                ViewBag.session = HttpContext.Session.GetString("username");
                var model = services.GetUser(user.Username);
                return View(model);
            }
            ViewBag.session = HttpContext.Session.GetString("username");
            ViewBag.data = services.GetUser(uname);
            return View();

        }

        [HttpGet]
        public IActionResult AdminDetailUser(int id)
        {
            string json_admin_session = HttpContext.Session.GetString("admin_session");
            JObject jsonResponseAdmin = null;
            Admins admin = null;
            if (json_admin_session != null)
            {
                //lấy session Admin
                jsonResponseAdmin = JObject.Parse(json_admin_session);
                admin = JsonConvert.DeserializeObject<Admins>(jsonResponseAdmin.ToString());

                if (admin != null)
                {
                    ViewBag.session = HttpContext.Session.GetString("username");
                    var model = uservices.GetUserRoleDepartments(id);
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Login", "Admin");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult ChangePassword(string uname)
        {
            string json_user_session = HttpContext.Session.GetString("users_session");
            JObject jsonResponseUser = null;
            Users user = null;
            if (json_user_session != null)
            {
                //lấy session Admin
                jsonResponseUser = JObject.Parse(json_user_session);
                user = JsonConvert.DeserializeObject<Users>(jsonResponseUser.ToString());

                if (user != null)
                {
                    ViewBag.session = HttpContext.Session.GetString("username");
                    var model = services.GetUser(uname);
                    return View(model);
                }
                else
                {
                    return RedirectToAction("User", "Profile");
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(Users editUser, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file.Length > 0)
                    {
                        var filepath = Path.Combine("wwwroot/images", file.FileName);
                        var stream = new FileStream(filepath, FileMode.Create);
                        file.CopyToAsync(stream);
                        editUser.Images = "images/" + file.FileName;
                        services.UpdateProfile(editUser);
                        return RedirectToAction("Profile", "User");
                    }
                    else
                    {
                        ViewBag.Msg = "Cannot change password";
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
            return RedirectToAction("Profile", "User");
        }

        public IActionResult FAQ()
        {
            return View();
        }
    }
}

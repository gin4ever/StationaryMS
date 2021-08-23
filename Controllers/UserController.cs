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
        public IActionResult Index()
        {
            var model = services.GetUsers();
            return View(model);

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
            Users acc = services.checkLogin(user);
            try
            {
                if (acc != null)
                {
                    HttpContext.Session.SetInt32("id", user.User_Id);
                    HttpContext.Session.SetString("username", user.Username);
                    HttpContext.Session.SetString("password", user.Password);
                    HttpContext.Session.SetString("users_session", JsonConvert.SerializeObject(acc));
                    return RedirectToAction("Profile", user);
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Profile(Users user)
        {
            var model = services.GetUser(user.Username);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
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
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Admin");
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(Users newUser, IFormFile file)
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
                        newUser.Images = "images/" + file.FileName;
                        services.createUser(newUser);
                        return RedirectToAction("AdminIndexUser");
                    }
                    else
                    {
                        ViewBag.Msg = "Cannot create new User";
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
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
                    return RedirectToAction("Login", "Profile");
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
                        return RedirectToAction("Profile");
                    }
                    else
                    {
                        ViewBag.Msg = "Cannot update User";
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }
    }
}

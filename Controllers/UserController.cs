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
            try
            {
                if (services.checkLogin(user) != null)
                {
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
        public IActionResult Profile(Users user)
        {
            var model = services.GetUser(user.Username);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Users newUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.createUser(newUser);
                    ModelState.AddModelError(string.Empty, "Congratulation!");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
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
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Models;
using eProject.Services;
namespace eProject.Controllers
{
    public class UserController : Controller
    {
        private IUsersServices services;
        public UserController(IUsersServices services)
        {
            this.services = services;
        }
        public IActionResult Index()
        {
            var model = services.GetUsers();
            return View(model);
        }

        public IActionResult AdminIndexUser()
        {
            var model = services.GetUsers();
            return View(model);
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
    }
}

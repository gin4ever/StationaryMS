using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Services;
using eProject.Models;
namespace eProject.Controllers
{
    //[Route("Admin")]
    public class AdminController : Controller
    {
        private IAdminServices services;
        public AdminController(IAdminServices services)
        {
            this.services = services;
        }

        //[Route("Admin/Dashboard/Index")]
        public IActionResult Index()
        {
            var listAdmin = services.GetAdmins();
            return View(listAdmin);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Admins admin)
        {

            try
            {
                if (services.checkLogin(admin) != null)
                {
                    return RedirectToAction("Index");
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
        public IActionResult Details(int id)
        {
            var model = services.GetAdmin(id);
            return View(model);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Admins newAdmin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.createAdmin(newAdmin);
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

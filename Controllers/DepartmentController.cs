using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Services;
using eProject.Models;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace eProject.Controllers
{
    public class DepartmentController : Controller
    {
        private IDepartmentServices services;
        public DepartmentController(IDepartmentServices services)
        {
            this.services = services;
        }
        public IActionResult AdminIndexDepartment(string dname)
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
                    var listDep = services.GetDepartments();
                    if (string.IsNullOrEmpty(dname))
                    {
                        return View(listDep);
                    }
                    else
                    {
                        listDep = services.GetDepartments().Where(d => d.DepartmentName.ToLower().Contains(dname.ToLower())).ToList();
                        return View(listDep);
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
        public IActionResult AdminCreateDepartment()
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
        public IActionResult AdminCreateDepartment(Department newDepartment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.AddDepartment(newDepartment);
                    return RedirectToAction("AdminIndexDepartment");
                }
                else
                {
                    ViewBag.Msg = "Cannot create new Department";
                }
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
            return View();
        }

        [HttpGet]
        public IActionResult AdminEditDepartment(int id)
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
                    var model = services.GetDepartment(id);
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Login", "Admin");
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult AdminEditDepartment(Department editDepartment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.UpdateDepartment(editDepartment);
                    return RedirectToAction("AdminIndexDepartment");
                }
                else
                {
                    ViewBag.Msg = "Cannot update Department";
                }
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
            return View();
        }

        public IActionResult AdminDeleteDepartment(int id)
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
                    services.DeleteDepartment(id);
                    return RedirectToAction("AdminIndexDepartment");
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

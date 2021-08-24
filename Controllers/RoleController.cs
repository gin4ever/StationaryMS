using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Services;
using eProject.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace eProject.Controllers
{
    public class RoleController : Controller
    {
        private IRoleServices services;
        public RoleController(IRoleServices services)
        {
            this.services = services;
        }
        public IActionResult AdminIndexRole(string rname)
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
                    var listRole = services.GetRoles();
                    if (string.IsNullOrEmpty(rname))
                    {
                        return View(listRole);
                    }
                    else
                    {
                        listRole = services.GetRoles().Where(r => r.RoleName.ToLower().Contains(rname.ToLower())).ToList();
                        return View(listRole);
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
        public IActionResult AdminCreateRole()
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
        public IActionResult AdminCreateRole(Role newRole)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.AddRole(newRole);
                    return RedirectToAction("AdminIndexRole");
                }
                else
                {
                    ViewBag.Msg = "Cannot create new Role";
                }
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
            return View();
        }

        [HttpGet]
        public IActionResult AdminEditRole(int id)
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
                    var model = services.GetRole(id);
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
        public IActionResult AdminEditRole(Role editRole)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.UpdateRole(editRole);
                    return RedirectToAction("AdminIndexRole");
                }
                else
                {
                    ViewBag.Msg = "Cannot update Role";
                }
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
            return View();
        }

        public IActionResult AdminDeleteRole(int id)
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
                    services.DeleteRole(id);
                    return RedirectToAction("AdminIndexRole");
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

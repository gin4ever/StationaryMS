using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Services;
using eProject.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace eProject.Controllers
{
    public class SupplierController : Controller
    {
        private ISupplierServices services;
        public SupplierController(ISupplierServices services)
        {
            this.services = services;
        }
        public IActionResult AdminIndexSupplier(string sname)
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
                    var listSup = services.GetSuppliers();
                    if (string.IsNullOrEmpty(sname))
                    {
                        return View(listSup);
                    }
                    else
                    {
                        listSup = services.GetSuppliers().Where(s => s.SupplierName.ToLower().Contains(sname.ToLower())).ToList();
                        return View(listSup);
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
        public IActionResult AdminCreateSupplier()
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
        public IActionResult AdminCreateSupplier(Supplier newSupplier)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.AddSupplier(newSupplier);
                    return RedirectToAction("AdminIndexSupplier");
                }
                else
                {
                    ViewBag.Msg = "Cannot create new Supplier";
                }
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
            return View();
        }

        [HttpGet]
        public IActionResult AdminEditSupplier(string code)
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
                    var model = services.GetSupplier(code);
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
        public IActionResult AdminEditSupplier(Supplier editSupplier)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.UpdateSupplier(editSupplier);
                    return RedirectToAction("AdminIndexSupplier");
                }
                else
                {
                    ViewBag.Msg = "Cannot update Supplier";
                }
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
            return View();
        }

        public IActionResult AdminDeleteSupplier(string code)
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
                    services.DeleteSupplier(code);
                    return RedirectToAction("AdminIndexSupplier");
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

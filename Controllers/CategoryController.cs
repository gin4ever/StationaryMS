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
    public class CategoryController : Controller
    {
        private ICategoryServices services;
        public CategoryController(ICategoryServices services)
        {
            this.services = services;
        }
        public IActionResult AdminIndexCategory(string cname)
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
                    var listCat = services.GetCategories();
                    if (string.IsNullOrEmpty(cname))
                    {
                        return View(listCat);
                    }
                    else
                    {
                        listCat = services.GetCategories().Where(a => a.Description.ToLower().Contains(cname.ToLower())).ToList();
                        return View(listCat);
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
        public IActionResult AdminCreateCategory()
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
        public IActionResult AdminCreateCategory(Category newCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.AddCategory(newCategory);
                    return RedirectToAction("AdminIndexCategory");
                }
                else
                {
                    ViewBag.Msg = "Cannot create new Category";
                }
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
            return View();
        }

        [HttpGet]
        public IActionResult AdminEditCategory(int id)
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
                    var model = services.GetCategory(id);
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
        public IActionResult AdminEditCategory(Category editCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.UpdateCategory(editCategory);
                    return RedirectToAction("AdminIndexCategory");
                }
                else
                {
                    ViewBag.Msg = "Cannot update Category";
                }
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
            return View();
        }

        public IActionResult AdminDeleteCategory(int id)
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
                    services.DeleteCategory(id);
                    return RedirectToAction("AdminIndexCategory");
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

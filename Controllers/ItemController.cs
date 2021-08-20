using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Models;
using eProject.Services;
using X.PagedList;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace eProject.Controllers
{
    public class ItemController : Controller
    {
        private IItemServices services;
        private IItemCategorySupplierServices iservices;
        private ICategoryServices cservices;
        private IRoleServices rservices;
        private ISupplierServices sservices;
        public ItemController(IItemServices services, IItemCategorySupplierServices iservices, ICategoryServices cservices, IRoleServices rservices, ISupplierServices sservices)
        {
            this.services = services;
            this.iservices = iservices;
            this.cservices = cservices;
            this.rservices = rservices;
            this.sservices = sservices;
        }
        public IActionResult Index(int? page, string itemName)
        {
            int pageSize = 4;
            int pageNumber = page ?? 1;
            if (string.IsNullOrEmpty(itemName))
            {
                var itemList = services.GetItems().ToPagedList(pageNumber, pageSize);
                ViewBag.data = itemList;
            }
            else
            {
                var itemList = services.GetItems().Where
                    (a => a.Description.Contains(itemName)).OrderByDescending(a => a.Price).ToList().ToPagedList(pageNumber, pageSize);
                ViewBag.data = itemList;
            }
            return View();
        }

        public IActionResult AdminIndexItem(string itemName)
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
                    var listItem = services.GetItems();
                    if (string.IsNullOrEmpty(itemName))
                    {
                        return View(listItem);
                    }
                    else
                    {
                        listItem = services.GetItems().Where(i => i.Description.ToLower().Contains(itemName.ToLower())).ToList();
                        return View(listItem);
                    }
                }
                else
                {
                    return RedirectToAction("Login","Admin");
                }
            }
            return View();
        }

        public IActionResult AdminDetailsItem(string itemCode)
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
                    var model = iservices.GetItemCategorySupplier(itemCode);
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
        public IActionResult AdminCreateItem()
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
                    var listCategory = cservices.GetCategories().ToList();
                    ViewBag.listCat = new SelectList(listCategory, "Category_Id", "Description");
                    var listRole = rservices.GetRoles().ToList();
                    ViewBag.listRole = new SelectList(listRole, "Role_Id", "RoleName");
                    var listSup = sservices.GetSuppliers().ToList();
                    ViewBag.listSup = new SelectList(listSup, "SupplierCode", "SupplierName");
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
        public IActionResult AdminCreateItem(Item newItem, IFormFile file)
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
                        newItem.Images = "images/" + file.FileName;
                        services.CreateItem(newItem);
                        return RedirectToAction("AdminIndexItem");
                    }
                    else
                    {
                        ViewBag.Msg = "Cannot create new Item";
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
        public IActionResult AdminEditItem(string code)
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
                    var model = services.GetItem(code);
                    var listCategory = cservices.GetCategories().ToList();
                    ViewBag.listCat = new SelectList(listCategory, "Category_Id", "Description");
                    var listRole = rservices.GetRoles().ToList();
                    ViewBag.listRole = new SelectList(listRole, "Role_Id", "RoleName");
                    var listSup = sservices.GetSuppliers().ToList();
                    ViewBag.listSup = new SelectList(listSup, "SupplierCode", "SupplierName");
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
        public IActionResult AdminEditItem(Item editItem, IFormFile file)
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
                        editItem.Images = "images/" + file.FileName;
                        services.UpdateItem(editItem);
                        return RedirectToAction("AdminIndexItem");
                    }
                    else
                    {
                        ViewBag.Msg = "Cannot update Item";
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
            return View();
        }
    }
}

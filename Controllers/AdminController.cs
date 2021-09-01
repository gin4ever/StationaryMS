using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Services;
using eProject.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        public IActionResult Index(string name)
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
                    var listAdmin = services.GetAdmins();
                    if (string.IsNullOrEmpty(name))
                    {
                        return View(listAdmin);
                    }
                    else
                    {
                        listAdmin = services.GetAdmins().Where(a => a.Username.ToLower().Contains(name.ToLower())).ToList();
                        return View(listAdmin);
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
        public IActionResult Login(Admins admin)
        {
            Admins acc = services.checkLogin(admin);
            try
            {
                if (acc != null)
                {
                    HttpContext.Session.SetInt32("id", admin.Admin_Id);
                    HttpContext.Session.SetString("username", admin.Username);
                    HttpContext.Session.SetString("password", admin.Password);
                    HttpContext.Session.SetString("admin_session", JsonConvert.SerializeObject(acc));
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
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Details(string uname)
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
                    var model = services.GetAdmin(uname);
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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Admins newAdmin, IFormFile file)
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
                        newAdmin.Images = "images/" + file.FileName;
                        services.createAdmin(newAdmin);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Msg = "Cannot create new Admin";
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
        public IActionResult Edit(string uname)
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
                    var model = services.GetAdmin(uname);
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
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Admins editAdmin, IFormFile file)
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
                        editAdmin.Images = "images/" + file.FileName;

                        var currentpass = services.GetAdmin(editAdmin.Username).Password;
                        ViewBag.pass = currentpass;
                        var identifypass = PinCodeSecurity.pinEncrypt(editAdmin.Password);
                        if (currentpass == identifypass)
                        {
                            var newpass = PinCodeSecurity.pinEncrypt(editAdmin.NewPassword);
                            var confirmnewpass = PinCodeSecurity.pinEncrypt(editAdmin.ConfirmPassword);
                            if (currentpass != newpass)
                            {
                                services.updateAdmin(editAdmin);
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                ViewBag.errorPass = "Your new password must different with your current password!";
                                return View("Edit");
                            }
                        }
                        else
                        {
                            ViewBag.errorPassIdentify = "Your password not correct!";
                            return View("Edit");
                        }
                    }
                    else
                    {
                        ViewBag.Msg = "Cannot update Admin";
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

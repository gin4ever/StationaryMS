using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Models;
using eProject.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace eProject.Controllers
{
    public class UserController : Controller
    {
        private IUsersServices services;
        public UserController(IUsersServices services)
        {
            this.services = services;
        }
        public IActionResult Index(string name)
        {

            string json_user_session = HttpContext.Session.GetString("user_session");
            JObject jsonResponseUser = null;
            Users user = null;
            if (json_user_session != null)
            {
                //lấy session User
                jsonResponseUser = JObject.Parse(json_user_session);
                user = JsonConvert.DeserializeObject<Users>(jsonResponseUser.ToString());

                if (user != null)
                {
                    ViewBag.session = HttpContext.Session.GetString("username");
                    var listAdmin = services.GetUsers();
                    if (string.IsNullOrEmpty(name))
                    {
                        return View(listAdmin);
                    }
                    else
                    {
                        listAdmin = services.GetUsers().Where(a => a.Username.ToLower().Contains(name.ToLower())).ToList();
                        return View(listAdmin);
                    }
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }
            }
            return View();
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
                var model = new Users
                {
                    Username = user.Username,
                    Password = user.Password
                };

                Users acc = services.checkLogin(model);
                if (acc != null)
                {
                    HttpContext.Session.SetInt32("id", user.User_Id);
                    HttpContext.Session.SetString("username", user.Username);
                    HttpContext.Session.SetString("password", user.Password);
                    HttpContext.Session.SetString("user_session", JsonConvert.SerializeObject(acc));
                    return RedirectToAction("Index","Request");
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
                
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Login");
            }


        }

        //log out
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    
        //hien thi profile
        [HttpGet]
        public IActionResult Profile(string uname)
        {
            //string json_user_session = HttpContext.Session.GetString("user_session");
            //JObject jsonResponseUser = null;
            //Users user = null;
            //if (json_user_session != null)
            //{
            //    //lấy session User
            //    jsonResponseUser = JObject.Parse(json_user_session);
            //    user = JsonConvert.DeserializeObject<Users>(jsonResponseUser.ToString());

            //    if (user != null)
            //    {
            //        ViewBag.session = HttpContext.Session.GetString("username");
            //        var model = services.GetUser(uname);
            //        return View(model);
            //    }
            //    else
            //    {
            //        return RedirectToAction("Login", "User");
            //    }
            //}


            string json_user_session = HttpContext.Session.GetString("user_session");
            JObject jsonResponseUser = null;
            Users user = null;
            if (json_user_session != null)
            {
                //lấy session User
                jsonResponseUser = JObject.Parse(json_user_session);
                user = JsonConvert.DeserializeObject<Users>(jsonResponseUser.ToString());
                ViewBag.session = HttpContext.Session.GetString("username");
            }
            ViewBag.session = HttpContext.Session.GetString("username");
            ViewBag.data = services.GetUser(uname);
            return View();
           
        }

    }
}

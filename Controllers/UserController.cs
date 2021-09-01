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
        private IRoleServices roleservices;
        private IDepartmentServices departmentservices;

        private IUserRoleDepartment uservices;
        public UserController(IDepartmentServices departmentservices, IRoleServices roleservices, IUsersServices services,
            IUserRoleDepartment uservices)
        {
            this.services = services;
            this.roleservices = roleservices;
            this.departmentservices = departmentservices;

            this.uservices = uservices;
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

        //log in
        [HttpGet]
        public IActionResult Login()
        {
            //notification
            ViewBag.alertlogin = TempData["alertlogin"];
            ViewBag.messagelogin = TempData["messagelogin"];
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
                int countRole = services.CountRole(acc);
                if (acc != null)
                {
                    HttpContext.Session.SetInt32("id", user.User_Id);
                    HttpContext.Session.SetString("username", user.Username);
                    HttpContext.Session.SetString("password", user.Password);
                    HttpContext.Session.SetInt32("approver", user.Department_Id);
                    HttpContext.Session.SetInt32("currentRole", user.Role_Id);

                    HttpContext.Session.SetString("user_session", JsonConvert.SerializeObject(acc));
                    return RedirectToAction("Profile","User");
                }
                else
                {
                    //notification
                    List<string> login = new List<string>();
                    login.Add("Recheck your username or password!");
                    TempData["alertlogin"] = "warning";
                    TempData["messagelogin"] = login;
                    return RedirectToAction("Login", "User");
                }
            }
            catch (Exception e)
            {
                //notification
                List<string> login = new List<string>();
                login.Add("Recheck your username or password!");
                TempData["alertlogin"] = "warning";
                TempData["messagelogin"] = login;
                return RedirectToAction("Login", "User");
            }


        }

        //log out
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    
        //show profile
        [HttpGet]
        public IActionResult Profile(Users user)
        {
            try { 
                    string json_user_session = HttpContext.Session.GetString("user_session");
                    JObject jsonResponseUser = null;
                    //Users user = null;
                    if (json_user_session != null)
                    {
                        //get session User
                        jsonResponseUser = JObject.Parse(json_user_session);
                        user = JsonConvert.DeserializeObject<Users>(jsonResponseUser.ToString());
                        var username = HttpContext.Session.GetString("username");
                        ViewBag.session = username;
                       
                        Users userinfo = services.GetUser(username);
                        ViewBag.info = userinfo;
                        ViewBag.role = roleservices.GetRole(user.Role_Id).RoleName;
                        ViewBag.department = departmentservices.GetDepartment(user.Department_Id).DepartmentName;
                        return View(userinfo);
                    }
                    else
                    {
                        return RedirectToAction("Login", "User");
                    }
                }
            catch (Exception e)
            {
                return RedirectToAction("Login", "User");
            }

        }
        //create account for testing
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
        public IActionResult ChangePassword(string uname)
        {
            string json_user_session = HttpContext.Session.GetString("user_session");
            JObject jsonResponseUser = null;
            Users user = null;
            if (json_user_session != null)
            {
                //get session user
                jsonResponseUser = JObject.Parse(json_user_session);
                user = JsonConvert.DeserializeObject<Users>(jsonResponseUser.ToString());
                if (user != null)
                {
                    ViewBag.session = HttpContext.Session.GetString("username");
                    var model = services.GetUser(uname);
                    //notification
                    ViewBag.alertchangepass = TempData["alertchangepass"];
                    ViewBag.messagechangepass = TempData["messagechangepass"];
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(Users editUser)
        {
            try
            {
                string json_user_session = HttpContext.Session.GetString("user_session");
                JObject jsonResponseUser = null;
                Users user = null;
                if (json_user_session != null)
                {
                    //get session User
                    jsonResponseUser = JObject.Parse(json_user_session);
                    user = JsonConvert.DeserializeObject<Users>(jsonResponseUser.ToString());
                    ViewBag.session = HttpContext.Session.GetString("username");
                    if (user == null)
                    {
                        return RedirectToAction("Index", "Login");
                    }
                    
                        List<string> changepass = new List<string>();
                        var currentpass = services.GetUser(editUser.Username).Password;
                        var identifypass = PinCodeSecurity.pinEncrypt(editUser.Password);
                    if (currentpass == identifypass)
                    {
                        var newpass = PinCodeSecurity.pinEncrypt(editUser.NewPassword);
                        var confirmnewpass = PinCodeSecurity.pinEncrypt(editUser.ConfirmPassword);
                        if (newpass != currentpass)
                        {
                            if (newpass == confirmnewpass)
                            {
                                services.UpdateProfile(editUser);
                                //notification
                                changepass.Add("Password is updated successfully!");
                                TempData["alertchangepass"] = "success";
                                TempData["messagechangepass"] = changepass;
                            }
                            else
                            {
                                //notification
                                changepass.Add("New password and confirm new password must be matched!");
                                TempData["alertchangepass"] = "danger";
                                TempData["messagechangepass"] = changepass;
                            }
                        }
                        else
                        {
                            //notification
                            changepass.Add("Your password is not changed compare with previous one. Change your password!");
                            TempData["alertchangepass"] = "danger";
                            TempData["messagechangepass"] = changepass;
                        }
                    }
                    else
                    {
                        //notification
                        changepass.Add("Recheck your current password!");
                        TempData["alertchangepass"] = "danger";
                        TempData["messagechangepass"] = changepass;
                    }
                    return Redirect(Request.Headers["Referer"].ToString());
                }
                
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
            return View();
        }

        public IActionResult FAQ()
        {

            string json_user_session = HttpContext.Session.GetString("user_session");
            JObject jsonResponseUser = null;
            Users user = null;

            if (json_user_session != null)
            {
                    //get session User
                    jsonResponseUser = JObject.Parse(json_user_session);
                    user = JsonConvert.DeserializeObject<Users>(jsonResponseUser.ToString());
                    ViewBag.session = HttpContext.Session.GetString("username");
                if (user != null)
                    {
                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }
            
            return RedirectToAction("Login", "User");
        }


        // -------------- Admin ---------------
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

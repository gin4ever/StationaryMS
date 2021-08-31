using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Models;
using eProject.Services;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
namespace eProject.Controllers
{
    public class ReportController : Controller
    {
        private IRequestItemServices services;
        public ReportController(IRequestItemServices services)
        {
            this.services = services;
        }
        public IActionResult Index(string Submit, DateTime fromDate, DateTime toDate)
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
                    var listReport = services.GetRequestItems();
                    if (Submit == null || Submit.Equals("Search"))
                    {
                        return View(listReport);
                    }
                    else
                    {
                        listReport = (from x in listReport where (x.DateRequest >= fromDate) && (x.DateRequest <= toDate) select x).ToList();
                        return View(listReport);
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Admin");
                }
            }
            return View();
        }
        //report cua User
        public IActionResult Report(string Submit, DateTime fromDate, DateTime toDate)
        {
            string json_users_session = HttpContext.Session.GetString("users_session");
            JObject jsonResponseUser = null;
            Users users = null;
            if (json_users_session != null)
            {
                //lấy session User
                jsonResponseUser = JObject.Parse(json_users_session);
                users = JsonConvert.DeserializeObject<Users>(jsonResponseUser.ToString());

                if (users != null)
                {
                    ViewBag.session = HttpContext.Session.GetString("username");
                    var listReport = services.GetRequestItems();
                    if (Submit == null || Submit.Equals("Search"))
                    {
                        return View(listReport);
                    }
                    else
                    {
                        listReport = (from x in listReport where (x.DateRequest >= fromDate) && (x.DateRequest <= toDate) select x).ToList();
                        return View(listReport);
                    }
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }
            }
            return View();
        }
    }
}

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
using X.PagedList;
using Rotativa;

namespace eProject.Controllers
{
    public class ReportController : Controller
    {
        private IRequestItemServices services;
        private IRequestDetailServices requestdetailservices;
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

        public IActionResult TaskList(string uname)
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
                ViewBag.itemList = services.GetRequestbyApprover(user.User_Id).ToList();
                return View();
            }
            return View("~/Views/User/Login.cshtml");
        }

        public IActionResult ReportList(string Submit, DateTime fromDate, DateTime toDate, string uname)
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
                ViewBag.itemList = services.GetRequestbyApprover(user.User_Id).ToList(); 
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
            return View("~/Views/User/Login.cshtml");
        }

        [Route("Index/Details/{id?}")]
        public IActionResult Details(int id, int? page, string itemName)
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
            }
            int pageSize = 10;
            int pageNumber = page ?? 1;
            //count total amount of request
            List<RequestDetail> item = requestdetailservices.GetRequestDetails(id).ToList();
            int count = item.ToList().Count;
            decimal TotalAmount = 0;
            for (int i = 0; i < count; i++)
            {
                TotalAmount += item[i].Total;
            }

            if (string.IsNullOrEmpty(itemName))
            {
                ViewBag.itemList = requestdetailservices.GetRequestDetails(id).ToList().ToPagedList(pageNumber, pageSize);
                ViewBag.requestTotal = TotalAmount;
                return View();
            }
            else
            {
                ViewBag.requestTotal = TotalAmount;
                ViewBag.itemList = requestdetailservices.GetRequestDetails(id).Where
                    (c => c.ItemCode.ToUpper().Contains(itemName.ToUpper()) ||
                c.ItemCode.ToLower().Contains(itemName.ToLower()) ||
                c.ItemCode.Equals(itemName)).ToList().ToPagedList(pageNumber, pageSize);
            }
            return View();
        }
    }
}

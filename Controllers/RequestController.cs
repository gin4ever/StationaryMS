using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using eProject.Models;
using Newtonsoft.Json;
using X.PagedList;

namespace eProject.Controllers
{
    public class RequestController : Controller
    {
        private IRequestServices services;
        private IRequestItemServices riServices;
        private IRequestDetailServices requestdetailservices;
        private IItemServices itemServices;
        private IUsersServices userservices;
        public RequestController(IRequestServices services, IRequestItemServices riServices, 
            IRequestDetailServices requestdetailservices, IItemServices itemServices, IUsersServices userservices)
        {
            this.services = services;
            this.riServices = riServices;
            this.requestdetailservices = requestdetailservices;
            this.itemServices = itemServices;
            this.userservices = userservices;
        }
        public IActionResult AdminIndexRequest()
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
                    var model = services.GetRequests();
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Login", "Admin");
                }
            }
            return View();
        }

        public IActionResult AdminDetailsRequest(int id)
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
                    var model = riServices.GetRequestItem(id);
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Login", "Admin");
                }
            }
            return View();
        }

        public IActionResult Index(string uname, int? page, string keyword)
        {
            List<string> messages = new List<string>();

            string json_user_session = HttpContext.Session.GetString("user_session");
            JObject jsonResponseUser = null;
            Users user = null;
            if (json_user_session != null)
            {
                //lấy session User
                jsonResponseUser = JObject.Parse(json_user_session);
                user = JsonConvert.DeserializeObject<Users>(jsonResponseUser.ToString());
                ViewBag.session = HttpContext.Session.GetString("username");
                //test xem co session khong
                messages.Add(uname);
                TempData["alert"] = "danger";
                TempData["message"] = messages;
                ViewBag.Alert = TempData["alert"];
                ViewBag.Message = TempData["message"];
            }

            //hien thi noi dung
            int pageSize = 4;
            int pageNumber = page ?? 1;
            if (string.IsNullOrEmpty(keyword))
            {
                ViewBag.itemList = services.GetRequestsByUserId(user.User_Id).ToList().ToPagedList(pageNumber, pageSize);
                return View();
            }
            else
            {
                ViewBag.itemList = services.GetRequestsByUserId(user.User_Id).Where
                (c => c.Status.ToUpper().Contains(keyword.ToUpper()) ||
                c.Status.ToLower().Contains(keyword.ToLower()) ||
                c.Status.Equals(keyword)).ToList().ToPagedList(pageNumber, pageSize);
                return View();
            }
        }

        //create new request
        public IActionResult Create(int? page, string itemName)
        {
            List<string> messages = new List<string>();
            string json_user_session = HttpContext.Session.GetString("user_session");
            JObject jsonResponseUser = null;
            Users user = null;
            if (json_user_session != null)
            {
                //get session User
                jsonResponseUser = JObject.Parse(json_user_session);
                user = JsonConvert.DeserializeObject<Users>(jsonResponseUser.ToString());
                ViewBag.session = HttpContext.Session.GetString("username");

                //test xem co session khong
                messages.Add(user.Username);
                TempData["alert"] = "danger";
                TempData["message"] = messages;
                ViewBag.Alert = TempData["alert"];
                ViewBag.Message = TempData["message"];
            }

            int pageSize = 4;
            int pageNumber = page ?? 1;
            if (string.IsNullOrEmpty(itemName))
            {
                var itemList = itemServices.GetItems().ToPagedList(pageNumber, pageSize);
                ViewBag.data = itemList;
            }
            else
            {
                var itemList = itemServices.GetItems().Where
                    (a => a.Description.Contains(itemName)).OrderByDescending(a => a.Price).ToList().ToPagedList(pageNumber, pageSize);
                ViewBag.data = itemList;
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddToCart(string id, int quantity, string button)
        {

            // add json sản phẩm vào session sau đó redirect_back về trang vừa thực hiện
            Item itemdetail = itemServices.GetItem(id);
            string json = HttpContext.Session.GetString("cart");
            List<CartItem> listCart = new List<CartItem>();

            if (json != null)
            {
                JArray jsonResponse = JArray.Parse(json);
                foreach (var item in jsonResponse)
                {
                    JObject cartResult = (JObject)item;
                    listCart.Add(JsonConvert.DeserializeObject<CartItem>(cartResult.ToString()));
                }
            }

            if (button == "delete")
            {
                int delete_index = -1;
                foreach (CartItem item in listCart)
                {
                    delete_index++;
                    if (item.Item.ItemCode == id)
                    {
                        break;
                    }
                }

                listCart.RemoveAt(delete_index);
            }
            else
            {
                CartItem cart = listCart.Where(i => i.Item.ItemCode.Equals(id)).FirstOrDefault();

                if (listCart.Count > 0 && cart != null)
                {
                    cart.Quantity = quantity;
                }
                else
                {
                    CartItem newItem = new CartItem { Item = itemdetail, Quantity = quantity, Price = itemdetail.Price };
                    listCart.Add(newItem);
                }
            }

            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(listCart));
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Submit()
        {

            //notification
            ViewBag.Alert = TempData["alert"];
            ViewBag.Message = TempData["message"];
            return View();
        }

        [HttpPost]
        public IActionResult SubmitRequest(Checkout checkout)
        {

            List<string> messages = new List<string>();
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
                //test xem co session khong
                messages.Add(user.Username);
                TempData["alert"] = "danger";
                TempData["message"] = messages;

            }
            //  get cart session 
            List<CartItem> listCart = new List<CartItem>();
            decimal total_amount = 0;
            string json_cart = HttpContext.Session.GetString("cart");
            if (json_cart != null)
            {
                JArray jsonResponseCart = JArray.Parse(json_cart);
                foreach (var item in jsonResponseCart)
                {
                    JObject cartResult = (JObject)item;
                    CartItem cart = JsonConvert.DeserializeObject<CartItem>(cartResult.ToString());
                    listCart.Add(cart);
                    total_amount += cart.Price * cart.Quantity;
                }

                //test xem co session khong
                messages.Add(user.Username);
                TempData["alert"] = "danger";
                TempData["message"] = messages;
            }

            // create order with item list and quantity
            Request request = new Request();
            if (user != null)
            {
                request.User_Id = user.User_Id;
                //test xem co session khong
                messages.Add(request.User_Id.ToString());
                TempData["alert"] = "danger";
                TempData["message"] = messages;
            }

            request.Reason = checkout.Reason;
            request.DateRequest = DateTime.Now;
            request.Status = "Pending";
            int request_id = services.SaveRequest(request);
            //test xem co session khong
            messages.Add(request_id.ToString());
            TempData["alert"] = "danger";
            TempData["message"] = messages;
            if (request_id > 0)
            {
                messages.Add(user.Username);
                TempData["alert"] = "danger";
                TempData["message"] = messages;
                //    // create relationship among RequestDetail , Request and Item
                //    foreach (CartItem cart in listCart)
                //    {
                //        //RequestDetail reqDetail = new RequestDetail
                //        //{
                //        //    Id = request_id,
                //        //    ItemCode = cart.Item.ItemCode,
                //        //    Price = cart.Price,
                //        //    Quantity = cart.Quantity
                //        //};
                //        //requestdetailservices.SaveRequestDetail(reqDetail);
                //        ////test xem co session khong

                //    }

                //    // remove cart session 
                //    HttpContext.Session.Remove("cart");

                //    // return success message
                //    messages.Add("Check out success. Click <a href='" + Url.Action("Detail", "Request", new { request_id = request_id }) + "'><u>here</u></a> to review your request");
                //    TempData["alert"] = "success";
                //    TempData["message"] = messages;
                //}
                //else
                //{
                //    // return failure message
                //    messages.Add("Can not submit order");
                //    TempData["alert"] = "danger";
                //    TempData["message"] = messages;
            }
            return RedirectToAction("Submit", "Request");
        }

        [HttpGet]
        [Route("Index/Details/{id?}")]
        public IActionResult Details(int id, int? page, string itemName)
        {
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

            int pageSize = 4;
            int pageNumber = page ?? 1;
            if (string.IsNullOrEmpty(itemName))
            {
                var itemList = requestdetailservices.GetRequestDetails(id).ToPagedList(pageNumber, pageSize);
                ViewBag.data = itemList;
            }
            else
            {
                var itemList = requestdetailservices.GetRequestDetails(id).Where
                    (a => a.Request_Id.Equals(itemName)).ToList().ToPagedList(pageNumber, pageSize);
                ViewBag.data = itemList;
            }
            return View();
        }
    }
}

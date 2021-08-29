using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Services;
using X.PagedList;
using eProject.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace eProject.Controllers
{
    public class RequestController : Controller
    {
        private IRequestServices services;
        private IRequestDetailServices requestdetailservices;
        private IItemServices itemServices;
        private IUsersServices userservices;
        public RequestController(IRequestServices services, IRequestDetailServices requestdetailservices, IItemServices itemServices, IUsersServices userservices)
        {
            this.services = services;
            this.requestdetailservices = requestdetailservices;
            this.itemServices = itemServices;
            this.userservices = userservices;
        }
        public IActionResult AdminIndexRequest()
        {
            var model = services.GetRequests();
            return View(model);
        }
        //request list
        public IActionResult Index(int? page, string keyword)
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
                //show content
                int pageSize = 10;
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
            return View("~/Views/User/Login.cshtml");
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
            }

                int pageSize = 10;
                int pageNumber = page ?? 1;
                if (string.IsNullOrEmpty(itemName))
                {
                    ViewBag.itemList = itemServices.GetItems().Where(a=>a.Role_Id.Equals(user.Role_Id)).ToPagedList(pageNumber, pageSize);
                    return View();
                }
                else
                {
                    ViewBag.itemList = itemServices.GetItems().Where
                        (a => a.Role_Id.Equals(user.Role_Id)&&( a.Description.Equals(itemName)||a.Description.ToUpper().Contains(itemName.ToUpper())||
                        a.Description.ToLower().Contains(itemName.ToLower()))).OrderByDescending(a => a.Price).ToList().ToPagedList(pageNumber, pageSize);
                }
            return View();
        }

        //add selected item to cart
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

        //go to submit page
        public IActionResult Submit()
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

            //notification
            ViewBag.Alert = TempData["alert"];
            ViewBag.Message = TempData["message"];
            return View();
        }

        //post request
        [HttpPost]
        [ActionName("Submit")]
        public IActionResult SubmitRequest(Request request)
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
            }
            else
            {
                return RedirectToAction("Create", "Request");
            }
            if (user != null)
            {
                request.Reason = request.Reason;
                request.DateRequest = DateTime.Now;
                request.User_Id = user.User_Id;
                request.ApprovedDate = null;
                int approverRole = user.Role_Id + 1;
                int deptcode = user.Department_Id;
                try
                {
                    Users approverId = userservices.GetUserByRoleID(approverRole, deptcode);
                    if (approverId!=null)
                    {
                        request.Approver = approverId.User_Id;
                        request.Status = "Pending";
                    }
                    else
                    {
                        request.Approver = user.User_Id;
                        request.Status = "Approved";
                        request.ApprovedDate = DateTime.Now;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                
                var requestID = services.SaveRequest(request);

                if (requestID > 0)
                {
                    // add info into request detail
                    foreach (CartItem cart in listCart)
                    {
                        RequestDetail reqDetail = new RequestDetail
                        {
                            Request_Id = requestID,
                            ItemCode = cart.Item.ItemCode,
                            Price = cart.Price,
                            Quantity = cart.Quantity,
                            Total = cart.Quantity * cart.Price
                        };
                        requestdetailservices.SaveRequestDetail(reqDetail);
                    }

                    // remove cart session 
                    HttpContext.Session.Remove("cart");

                    // return success message
                    messages.Add("Submit success. Click <a href='" + Url.Action("Details", "Request", new { id=requestID }) + "'><u>here</u></a> to review your request");
                    TempData["alert"] = "success";
                    TempData["message"] = messages;
                }
                else
                {
                    // return failure message
                    messages.Add("Can not submit order");
                    TempData["alert"] = "danger";
                    TempData["message"] = messages;
                }
            }
            return RedirectToAction("Submit", "Request");
        }

        //go to detail via ID
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
        
        //go to edit
        [HttpGet]
        [Route("Index/Edit/{id?}")]
        public IActionResult Edit(RequestDetail req, int? page, string itemName)
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
            //count total amount of request
            List<RequestDetail> item = requestdetailservices.GetRequestDetails(req.Id).ToList();
                int count = item.ToList().Count;
                decimal TotalAmount = 0;
                for (int i = 0; i < count; i++)
                {
                    TotalAmount += item[i].Total;
                }
                int pageSize = 10;
                int pageNumber = page ?? 1;
                if (string.IsNullOrEmpty(itemName))
                {
                    ViewBag.itemList = requestdetailservices.GetRequestDetails(req.Id).ToList().ToPagedList(pageNumber, pageSize);
                    ViewBag.requestTotal = TotalAmount;
                return View();
                }
                else
                {
                    ViewBag.itemList = requestdetailservices.GetRequestDetails(req.Id).Where
                        (c => c.ItemCode.ToUpper().Contains(itemName.ToUpper()) ||
                    c.ItemCode.ToLower().Contains(itemName.ToLower()) ||
                    c.ItemCode.Equals(itemName)).ToList().ToPagedList(pageNumber, pageSize);
                    ViewBag.requestTotal = TotalAmount;
            }
                return View();
            
        }

        //delete item in Request via View Index
        public IActionResult DeleteRequest(int id)
        {
            services.DeleteRequest(id);
            return RedirectToAction("Index");
        }

        //delete item in RequestDetail via View Edit
        public IActionResult DelItem(int id)
        {
            var model = requestdetailservices.GetItem(id);
            var requestId = model.Request_Id;
            List<RequestDetail> detail = requestdetailservices.GetRequestDetails(requestId).ToList();
            int countRequestItem = detail.Where(n => n.Request_Id.Equals(model.Request_Id)).ToList().Count;
            requestdetailservices.DelItem(id);
            if (countRequestItem == 1)
            {
                return RedirectToAction("Index");
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }

        //[HttpGet]
        //[Route("Index/Edit/{req?}/Update/{id?}")]
        public IActionResult Update(int id)
        {
            RequestDetail model = requestdetailservices.GetItem(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(RequestDetail request)
        {
            RequestDetail model = requestdetailservices.GetItem(request.Id);
            requestdetailservices.UpdateRequestDetail(request);
            return RedirectToAction("Edit", new { id = model.Request_Id });
        }


        public IActionResult TaskList(int? page, string keyword)
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
                //show content
                int pageSize = 10;
                int pageNumber = page ?? 1;
                if (string.IsNullOrEmpty(keyword))
                {
                    
                    List<Request> taskList = services.GetRequestsByApproverID(user.User_Id);
                    ViewBag.itemList = taskList.ToPagedList(pageNumber, pageSize);
                    return View();
                }
                //else
                //{
                //    ViewBag.itemList = services.GetRequestsByUserId(user.User_Id).Where
                //    (c => c.Status.ToUpper().Contains(keyword.ToUpper()) ||
                //    c.Status.ToLower().Contains(keyword.ToLower()) ||
                //    c.Status.Equals(keyword)).ToList().ToPagedList(pageNumber, pageSize);
                //    return View();
                //}
            }
            return View("~/Views/User/Login.cshtml");
        }
    }
}

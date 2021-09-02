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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eProject.Controllers
{
    public class RequestController : Controller
    {
        private IRequestServices services;
        private IRequestDetailServices requestdetailservices;
        private IItemServices itemServices;
        private IUsersServices userservices;
        private IDepartmentServices departmentservices;
        private IRoleServices roleservices;
        private IRequestItemServices riServices;
        public RequestController(IRoleServices roleservices, IDepartmentServices departmentservices, 
            IRequestServices services, IRequestDetailServices requestdetailservices, 
            IItemServices itemServices, IUsersServices userservices, IRequestItemServices riServices)
        {
            this.services = services;
            this.requestdetailservices = requestdetailservices;
            this.itemServices = itemServices;
            this.userservices = userservices;
            this.departmentservices = departmentservices;
            this.roleservices = roleservices;
            this.riServices = riServices;
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

                //notification
                ViewBag.AlertDelRequest = TempData["alertmessDelRequest"];
                ViewBag.MessageDelRequest = TempData["messagemessDelRequest"];
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
                }

                return View();
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
                    ViewBag.itemList = itemServices.GetItems().Where(a=>a.Role_Id<=(user.Role_Id)).ToPagedList(pageNumber, pageSize);
                    return View();
                }
                else
                {
                    ViewBag.itemList = itemServices.GetItems().Where
                        (a => a.Role_Id<=(user.Role_Id)&&( a.Description.Equals(itemName)||a.Description.ToUpper().Contains(itemName.ToUpper())||
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
                    messages.Add("Submit your request successfully. Click <a href='" + Url.Action("Details", "Request", new { id=requestID }) + "'><u>here</u></a> to review your request");
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
            List<vRequestItem> item = requestdetailservices.GetAllRequestDetails(id).ToList();
            //show requestId
            ViewBag.reqId = id;
            int count = item.ToList().Count;
            decimal TotalAmount = 0;
            for (int i = 0; i < count; i++)
            {
                TotalAmount += item[i].Total;
            }

            if (string.IsNullOrEmpty(itemName))
            {
                ViewBag.itemList = item.ToPagedList(pageNumber, pageSize);
                ViewBag.requestTotal = TotalAmount;
                return View();
            }
            else
            {
                ViewBag.requestTotal = TotalAmount;
                ViewBag.itemList = item.Where
                (c => c.ItemCode.ToUpper().Contains(itemName.ToUpper()) ||
            c.ItemCode.ToLower().Contains(itemName.ToLower()) ||
            c.ItemCode.Equals(itemName)).ToPagedList(pageNumber, pageSize);
            }
            return View();
        }
        

        //delete item in Request via View Index
        public IActionResult DeleteRequest(int id)
        {
            List<string> messDelRequest = new List<string>();
            var req = services.GetRequest(id);
            var status = req.Status;
            DateTime today = DateTime.Now;
            //we will proceed all request on 25th monthly, so user only can change it before 20 if it is not approved yet
            DateTime cutoffdate = new DateTime(today.Year, today.Month, 20);
            if (status== "Pending" || status =="Rejected")
            {
                services.DeleteRequest(id);
                //notification
                messDelRequest.Add("Remove action is succeeded.");
                TempData["alertmessDelRequest"] = "success";
                TempData["messagemessDelRequest"] = messDelRequest;
                return RedirectToAction("Index");
            }
            else
            {
                if (today < cutoffdate)
                {
                    services.DeleteRequest(id);
                    //notification
                    messDelRequest.Add("Remove action is succeeded.");
                    TempData["alertmessDelRequest"] = "success";
                    TempData["messagemessDelRequest"] = messDelRequest;
                    return RedirectToAction("Index");
                }
                else
                {
                    //notification
                    messDelRequest.Add("You missed cut off date.");
                    TempData["alertmessDelRequest"] = "danger";
                    TempData["messagemessDelRequest"] = messDelRequest;
                    return RedirectToAction("Index");
                }
            }
            //services.DeleteRequest(id);
            //return RedirectToAction("Index");
        }


        //go to edit
        [HttpGet]
        [Route("Index/Edit/{id?}")]
        public IActionResult Edit(vRequestItem req, int? page, string itemName)
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
            List<vRequestItem> item = requestdetailservices.GetAllRequestDetails(req.Id).ToList();
            int count = item.Count;
            decimal TotalAmount = 0;
            for (int i = 0; i < count; i++)
            {
                TotalAmount += item[i].Total;
            }
            //show requestId
            ViewBag.reqId = req.Id;
            ViewBag.requestTotal = TotalAmount;

            //notification
            ViewBag.AlertDelItem = TempData["alertDelItem"];
            ViewBag.MessageDelItem = TempData["messageDelItem"];

            //show page by search
            int pageSize = 10;
            int pageNumber = page ?? 1;
            if (string.IsNullOrEmpty(itemName))
            {
                ViewBag.itemList = item.ToPagedList(pageNumber, pageSize);
                
                return View();
            }
            else
            {
                ViewBag.itemList = item.Where
                    (c => c.Description.ToUpper().Contains(itemName.ToUpper()) ||
                c.Description.ToLower().Contains(itemName.ToLower()) ||
                c.Description.Equals(itemName)).ToPagedList(pageNumber, pageSize);
            }

            return View(req);

        }
        //delete item in RequestDetail via View Edit
        public IActionResult DelItem(int id)
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
            List<string> messDelItem = new List<string>();
            var model = requestdetailservices.GetItem(id);
            var requestId = model.Request_Id;
            List<RequestDetail> detail = requestdetailservices.GetRequestDetails(requestId).ToList();
            int countRequestItem = detail.Where(n => n.Request_Id.Equals(model.Request_Id)).ToList().Count;
            requestdetailservices.DelItem(id);


            if (countRequestItem == 1)
            {
                return RedirectToAction("Index");
            }
            //notification
            messDelItem.Add("Delete item action is succeeded.");
            TempData["alertDelItem"] = "success";
            TempData["messageDelItem"] = messDelItem;

            return Redirect(Request.Headers["Referer"].ToString());
        }

        //[HttpGet]
        public IActionResult Update(int id)
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
            RequestDetail model = requestdetailservices.GetItem(id);
            ViewBag.itemName = itemServices.GetItem(model.ItemCode).Description;
            ViewBag.itemImage = itemServices.GetItem(model.ItemCode).Images;
            //notification
            ViewBag.AlertUpdate = TempData["alertu"];
            ViewBag.MessageUpdate = TempData["messageu"];
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(RequestDetail request)
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
            List<string> messUpdate = new List<string>();
            var currentitemcode = request.Id;
            var currentitemquantity = requestdetailservices.GetItem(currentitemcode).Quantity;
            if (currentitemquantity!=request.Quantity)
            {
                requestdetailservices.UpdateRequestDetail(request);
                //notification
                messUpdate.Add("Item quantity is updated successfully!");
                TempData["alertu"] = "success";
                TempData["messageu"] = messUpdate;
            }
            else
            {
                //notification
                messUpdate.Add("You have not changed your Item quantity yet.");
                TempData["alertu"] = "danger";
                TempData["messageu"] = messUpdate;
            }
            return Redirect(Request.Headers["Referer"].ToString());
            //return RedirectToAction("Edit", new { id = model.Request_Id });
        }

        //task list for role >0
        public IActionResult TaskList(int? page, string Submit, DateTime fromDate, DateTime toDate)
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
                List<Request> taskList = services.GetRequestsByApproverID(user.User_Id).Where(a => a.Status.Equals("Pending") || a.Status.Equals("Forwarded")).ToList();
                
                //show content
                int pageSize = 10;
                int pageNumber = page ?? 1;
                if (Submit == null || Submit.Equals("Search"))
                {
                    ViewBag.itemList = taskList.ToPagedList(pageNumber, pageSize);
                    return View();
                }
                else
                {
                    ViewBag.itemList = taskList.Where(a => (a.DateRequest >= fromDate) && (a.DateRequest <= toDate)).ToPagedList(pageNumber, pageSize);
                    return View();
                }
            }
            return View("~/Views/User/Login.cshtml");
        }
        //action for role>0
        [HttpGet]
        public IActionResult Action(int id, int? page)
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
            Request req = services.GetRequest(item[0].Request_Id);
           

            int count = item.ToList().Count;
            decimal TotalAmount = 0;
            for (int i = 0; i < count; i++)
            {
                TotalAmount += item[i].Total;
            }
                var requesterId = userservices.GetUser(req.User_Id);
                ViewBag.reqId = req.Request_Id;
                ViewBag.itemList = requestdetailservices.GetRequestDetails(id).ToList().ToPagedList(pageNumber, pageSize);
                ViewBag.requestTotal = TotalAmount;
                ViewBag.requesterName = requesterId.Username;
                //notification
                ViewBag.AlertAction = TempData["alerta"];
                ViewBag.MessageAction = TempData["messagea"];
            return View();
        }

        [HttpPost]
        public IActionResult Action(RequestDetail requestdetail,  string submit)
        {
            List<string> mess = new List<string>();
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
            //search for nex approver
            int approverRole = user.Role_Id + 1;
            //search current department
            int deptcode = user.Department_Id;
            //get highest role in company
            List<Role> HighestApproberId = roleservices.GetRoles().OrderByDescending(a => a.Role_Id).ToList();
            var highestRole = HighestApproberId[0].Role_Id;
            var highestUserRole = userservices.GetUserByRole(highestRole);
            var highestUserId = highestUserRole.User_Id;
            //highest usderID in company
            var highetUser = userservices.GetUser(highestRole);
            //search UserID of next approver
            Users approverId = userservices.GetUserByRoleID(approverRole, deptcode);
            //get budget of current approver
            var budget = roleservices.GetRole(user.Role_Id).RoleBudget;

            //get model of current request
            Request req = services.GetRequest(requestdetail.Id);
            //find List of requestdetail in this request
            List<RequestDetail> getitems = requestdetailservices.GetRequestDetails(requestdetail.Id).ToList();
            //count number of requestdetail in this request
            int count = getitems.Count;
            //count total amount of request
            decimal TotalAmount = 0;
            for (int i = 0; i < count; i++)
            {
                TotalAmount += getitems[i].Total;
            }
            
            //proceed approver action
            if (approverId != null)
            {
                if (TotalAmount <= budget && submit.Equals("Approve"))
                {
                    req.Status = "Approved";
                    req.ApprovedDate = DateTime.Now;
                    // return success message
                    mess.Add("Approve action is succeeded.");
                    TempData["alerta"] = "success";
                    TempData["messagea"] = mess;

                }
                else if (TotalAmount >= budget && submit.Equals("Approve"))
                {
                    // return error message
                    mess.Add("Can not approve over your Role Limitation");
                    TempData["alerta"] = "danger";
                    TempData["messagea"] = mess;
                }
                else if (TotalAmount > budget && submit.Equals("Forward"))
                {
                    req.Status = "Forwarded";
                    req.ApprovedDate = DateTime.Now;
                    req.Approver = approverId.User_Id;

                    // return success message
                    mess.Add("Forward to " + @approverId.Username);
                    TempData["alerta"] = "success";
                    TempData["messagea"] = mess;
                }
                else if (TotalAmount <= budget && submit.Equals("Forward"))
                {
                    // return error message
                    mess.Add("Can not forward. This request amount is under your Role Limitation");
                    TempData["alerta"] = "danger";
                    TempData["messagea"] = mess;
                }
                else
                {
                    req.Status = "Rejected";
                    req.ApprovedDate = DateTime.Now;
                    // return success message
                    mess.Add("Reject action is succeeded.");
                    TempData["alerta"] = "success";
                    TempData["messagea"] = mess;
                }
                services.UpdateRequest(req);
                return RedirectToAction("Action", "Request");
            }
            else
            {
                
                if (submit.Equals("Approve"))
                {
                    req.Status = "Approved";
                    req.ApprovedDate = DateTime.Now;
                    req.Approver = highestUserId;
                    // return success message
                    mess.Add("Approve action is succeeded.");
                    TempData["alerta"] = "success";
                    TempData["messagea"] = mess;
                    services.UpdateRequest(req);
                }
                else if (submit.Equals("Forward"))
                {
                    // return error message
                    mess.Add("Can not forward. This request amount is under your Role Limitation");
                    TempData["alerta"] = "danger";
                    TempData["messagea"] = mess;
                }
                else
                {
                    req.Status = "Rejected";
                    req.ApprovedDate = DateTime.Now;
                    req.Approver = highestUserId;
                    // return success message
                    mess.Add("Reject action is succeeded.");
                    TempData["alerta"] = "success";
                    TempData["messagea"] = mess;
                    services.UpdateRequest(req);
                }
                
                return RedirectToAction("Action", "Request");
            }
            
        }
        //task history of role>0
        public IActionResult TaskHistory(int? page, string Submit, DateTime fromDate, DateTime toDate)
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

                int pageSize = 10;
                int pageNumber = page ?? 1;
                List<Request> taskList = services.GetRequestsByApproverID(user.User_Id).Where(a => a.Status.Equals("Rejeted") || a.Status.Equals("Approved") || a.Status.Equals("Forwarded")).ToList();
                if (Submit == null || Submit.Equals("Search"))
                {
                    ViewBag.itemList = taskList.ToList().ToPagedList(pageNumber, pageSize);
                    return View();
                }
                else
                {
                    ViewBag.itemList = taskList.Where(a => (a.DateRequest >= fromDate) && (a.DateRequest <= toDate)).ToPagedList(pageNumber, pageSize);
                    return View();
                }
            }
            return View("~/Views/User/Login.cshtml");
        }

        //report for role >0
        public IActionResult Report(string Filter, DateTime fromDate, DateTime toDate, string Product)
        {
            string json_user_session = HttpContext.Session.GetString("user_session");
            JObject jsonResponseUser = null;
            Users user = null;
            if (json_user_session != null)
            {
                //get session User
                jsonResponseUser = JObject.Parse(json_user_session);
                user = JsonConvert.DeserializeObject<Users>(jsonResponseUser.ToString());
                int headcount = userservices.GetUsers().Where(a=>a.Department_Id.Equals(user.Department_Id)).Count();
                ViewBag.item = itemServices.GetItems();
                List<vRequestItem> listReport = requestdetailservices.GetAllItembyDept(user.Department_Id).Where(a => a.Status.Equals("Approved")).ToList();

                List<Item> Item = itemServices.GetItems();
                SelectList itemList = new SelectList(Item, "Description", "Description");

                if (user != null)
                {
                    ViewBag.session = HttpContext.Session.GetString("username");
                    if (string.IsNullOrEmpty(Filter))
                    {

                        decimal TotalAmount = 0;
                        ViewBag.itemList = listReport.ToList();
                        ViewBag.Item = itemList;
                        ViewBag.requestTotal = TotalAmount;

                        int count = listReport.Count();

                        for (int i = 0; i < count; i++)
                        {
                            TotalAmount += listReport[i].Total;
                        }
                        ViewBag.requestTotal = TotalAmount;
                        ViewBag.percentage = Math.Round((TotalAmount / 100), 2);
                        ViewBag.perheadcount = Math.Round((TotalAmount / headcount), 2);
                       

                        return View();
                    }
                    else
                    {
                        ViewBag.Item = itemList;

                        decimal TotalAmount = 0;
                        var showlist = listReport.Where(a => ((a.DateRequest >= fromDate) || (a.DateRequest <= toDate)) && (a.Description == Product)).ToList();
                        ViewBag.itemList = showlist;
                        int count = showlist.Count();
                        ViewBag.productname = Product;
                        
                        for (int i = 0; i < count; i++)
                        {
                            TotalAmount += showlist[i].Total;
                        }
                        ViewBag.requestTotal = TotalAmount;
                        ViewBag.percentage = Math.Round((TotalAmount / 100),2);
                        ViewBag.perheadcount = Math.Round((TotalAmount / headcount),2);
                      
                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }
            }
            return View();
        }

        // ---------------- Admin ---------------
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

        [Route("AdminIndexRequest/AdminDetailsRequest/{id?}")]
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
                List<vRequestItem> item = requestdetailservices.GetAllRequestDetails(id).ToList();
                ViewBag.reqId = id;
                if (admin != null)
                {
                    ViewBag.session = HttpContext.Session.GetString("username");
                    int count = item.ToList().Count;
                    decimal TotalAmount = 0;
                    for (int i = 0; i < count; i++)
                    {
                        TotalAmount += item[i].Total;
                    }
                    ViewBag.itemList = item.ToList();
                    ViewBag.requestTotal = TotalAmount;
                    return View();
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

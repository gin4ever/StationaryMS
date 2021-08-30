using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.Models;
using eProject.Repository;
namespace eProject.Services
{
    public class RequestServices : IRequestServices
    {
        private StationeryContext context;
        private StationeryContext detailcontext;
        private StationeryContext usercontext;
        private StationeryContext departmentcontext;
        private StationeryContext rolecontext;


        public RequestServices(StationeryContext rolecontext, StationeryContext context, StationeryContext detailcontext, StationeryContext usercontext, StationeryContext departmentcontext)
        {
            this.context = context;
            this.detailcontext = detailcontext;
            this.usercontext = usercontext;
            this.departmentcontext = departmentcontext;
            this.rolecontext = rolecontext;
        }
        public List<Request> GetRequests()
        {
            List<Request> request = context.Request.OrderBy(a => a.DateRequest).ToList();
            return request;
        }

        public int CountRequest(int user_id)
        {
            return context.Request.Where(i => i.User_Id.Equals(user_id)).ToList().Count;

        }

        public Request GetRequest(int id)
        {
            return context.Request.SingleOrDefault(a => a.Request_Id == id);
        }


        public List<Request> GetRequestsByUserId(int user_id)
        {
            List<Request> requestByUser = context.Request.Where(i => i.User_Id.Equals(user_id)).OrderByDescending(a => a.DateRequest).ToList();
            return requestByUser;
        }

        public int SaveRequest(Request request)
        {
            try
            {
                context.Request.Add(request);
                context.SaveChanges();
                return request.Request_Id;
            }
            catch (Exception e)
            {
                return 0;
            }

        }

        public bool ApproveRequest(Request request)
        {
            var model = context.Request.SingleOrDefault(a => a.Request_Id == request.Request_Id);
            if (model == null)
            {
                return false;
            }
            model.Status = request.Status;
            context.SaveChanges();
            return true;

        }


        public bool DeleteRequest(int rqId)
        {
            var delequest = context.Request.SingleOrDefault(m => m.Request_Id.Equals(rqId));
          
            if (delequest != null)
            {
                List<RequestDetail> listRequestDetail = detailcontext.RequestDetail.Where(i => i.Request_Id.Equals(delequest.Request_Id)).ToList();
                var count = listRequestDetail.Count();
                for (int i = 0; i < count; i++)
                {
                    detailcontext.RequestDetail.Remove(listRequestDetail[i]);
                    detailcontext.SaveChanges();
                }
                
                context.Request.Remove(delequest);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }


        public List<Request> GetRequestsByApproverID(int user_id)
        {
            List<Request> requestByApprover = context.Request.Where(i => i.Approver.Equals(user_id)).OrderByDescending(a => a.DateRequest).ToList();
            return requestByApprover;
        }

        public bool UpdateRequest(Request updaterequest)
        {
            var updatereq = context.Request.SingleOrDefault(i => i.Request_Id.Equals(updaterequest.Request_Id));
            //List<RequestDetail> listRequestDetail = detailcontext.RequestDetail.Where(i => i.Request_Id.Equals(updaterequest.Request_Id)).ToList();

            //int count = listRequestDetail.ToList().Count;
            //decimal TotalAmount = 0;
            //for (int i = 0; i < count; i++)
            //{
            //    TotalAmount += listRequestDetail[i].Total;
            //}
            ////current approver user
            
            //int userId = updatereq.Role_Id + 1;
            ////search for next approver
            //var approverUser = usercontext.Users.Find(userId);
            ////search current department
            //var deptcode = departmentcontext.Department.Find(approverUser.Department_Id);
            ////get highest role in company
            //List<Role> HighestApproberId = rolecontext.Role.OrderByDescending(a => a.Role_Id).ToList();
            //var highestRole = HighestApproberId[0].Role_Id;
            ////search UserID of next approver
            //Users nextApprover = usercontext.Users.FirstOrDefault(a=>a.Department_Id.Equals(deptcode.Department_Id)&&a.Role_Id.Equals(approverUser.Role_Id));
            //int nextApproverUserID = nextApprover.User_Id;
            ////get budget of current approver
            //var budget = rolecontext.Role.FirstOrDefault();
            //decimal bud = budget.RoleBudget;


            //if (nextApproverUserID != highestRole)
            //{
            //    if (TotalAmount <= bud && submit.Equals("Approve"))
            //    {
            //        updatereq.Status = "Approved";
            //        updatereq.ApprovedDate = DateTime.Now;
            //        //Neu TotalAmount > budget => thong bao ban k duoc approve
            //    }
            //    else if (TotalAmount > bud && submit.Equals("Forward"))
            //    {
            //        updatereq.Status = "Forwarded";
            //        updatereq.ApprovedDate = DateTime.Now;
            //        updatereq.Approver = user.Role_Id + 1;
            //        //Neu TotalAmount > budget => forward to higher manager
            //    }
            //    else
            //    {
            //        updatereq.Status = "Rejected";
            //        updatereq.ApprovedDate = DateTime.Now;

            //    }
            //    services.UpdateRequest(req);
            //    return RedirectToAction("Index", "Request");

            //}
            //else
            //{

            //    if (submit.Equals("Approved"))
            //    {
            //        req.Status = "Approved";
            //        req.ApprovedDate = DateTime.Now;
            //        req.Approver = highestRole;
            //        //Neu TotalAmount > budget => thong bao ban k duoc approve
            //    }
            //    //else if (button.Equals("Forward"))
            //    //{
            //    //  //
            //    //}
            //    else
            //    {
            //        req.Status = "Rejected";
            //        req.ApprovedDate = DateTime.Now;
            //        req.Approver = highestRole;

            //    }
            //    services.UpdateRequest(req);
            //    return RedirectToAction("TaskList");



                if (updatereq != null)
            {
                updatereq.Status = updaterequest.Status;
                updatereq.ApprovedDate = updaterequest.ApprovedDate;
                updatereq.Approver = updaterequest.Approver;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}

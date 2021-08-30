using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.Models;
using eProject.Repository;
namespace eProject.Services
{
    public class RequestItemServices : IRequestItemServices
    {
        private StationeryContext context;
        public RequestItemServices(StationeryContext context)
        {
            this.context = context;
        }
        
        public List<Request> GetRequestbyApprover(int approveID)
        {
            List<Request> approveRequest = context.Request.Where(a => a.Approver.Equals(approveID)).ToList();
            return approveRequest;
        }

        public List<Request> GetReportByUserId(int user_id)
        {
            List<Request> reportByUser = context.Request.Where(i => i.User_Id.Equals(user_id)).OrderByDescending(a => a.DateRequest).ToList();
            return reportByUser;
        }

        public vRequestItem GetRequestItem(int id)
        {
            var model = context.vRequestItem.SingleOrDefault(m => m.Request_Id.Equals(id));
            if (model != null)
            {
                return model;
            }
            else
            {
                return null;
            }
        }

        public List<vRequestItem> GetRequestItems()
        {
            return context.vRequestItem.ToList();
        }

        public List<vRequestItem> GetRequestItemsByUserID(int user_id)
        {
            var model = context.vRequestItem.Where(i => i.User_Id.Equals(user_id)).ToList();
            if (model != null)
            {
                return model;
            }
            else
            {
                return null;
            }
        }
    }
}

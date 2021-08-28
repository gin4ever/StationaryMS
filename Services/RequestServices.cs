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

        public RequestServices(StationeryContext context, StationeryContext detailcontext)
        {
            this.context = context;
            this.detailcontext = detailcontext;
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
    }
}

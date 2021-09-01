using eProject.Models;
using eProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProject.Services
{
    public class RequestDetailServices : IRequestDetailServices
    {

        private StationeryContext requestcontext;
        private StationeryContext context;
        private StationeryContext vRequestItemcontext;
        public RequestDetailServices(StationeryContext requestcontext, StationeryContext context, StationeryContext vRequestItemcontext)
        {
            this.context = context;
            this.requestcontext = requestcontext;
            this.vRequestItemcontext = vRequestItemcontext;
        }

        public List<RequestDetail> GetRequestDetails(int reqId)
        {
            List<RequestDetail> listRequestDetail = context.RequestDetail.Where(i => i.Request_Id.Equals(reqId)).ToList();
            return listRequestDetail;
        }

        public List<vRequestItem> GetAllRequestDetails(int reqId)
        {
            List<vRequestItem> vRequestItem = vRequestItemcontext.vRequestItem.Where(i => i.Request_Id.Equals(reqId)).ToList();
            return vRequestItem;
        }

        public List<vRequestItem> GetAllItembyDept(int dep)
        {
            List<vRequestItem> vRequestItem = vRequestItemcontext.vRequestItem.Where(i => i.Department_Id.Equals(dep)).ToList();
            return vRequestItem;
        }

        public bool SaveRequestDetail(RequestDetail requestDetail)
        {
            try
            {
                context.RequestDetail.Add(requestDetail);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateRequestDetail(RequestDetail request)
        {
            var editrequest = context.RequestDetail.SingleOrDefault(i => i.Id.Equals(request.Id));
            
            if (editrequest != null)
            {
                editrequest.Quantity = request.Quantity;
                editrequest.Total = editrequest.Price * request.Quantity;
                context.SaveChanges();
                requestcontext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public RequestDetail GetItem(int rqId)
        {
            var model = context.RequestDetail.SingleOrDefault(m => m.Id.Equals(rqId));
            if (model != null)
            {
                return model;
            }
            else
            {
                return null;
            }
        }

        public bool DelItem(int id)
        {
            RequestDetail delitem = context.RequestDetail.SingleOrDefault(m => m.Id.Equals(id));
            int count = context.RequestDetail.Where(n => n.Request_Id.Equals(delitem.Request_Id)).ToList().Count;
            
            for (int i = 0; i < count-1; i++)
            {
                if (delitem != null)
                {
                    context.RequestDetail.Remove(delitem);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            var delequest = requestcontext.Request.SingleOrDefault(m => m.Request_Id.Equals(delitem.Request_Id));

            if (delitem != null)
            {
                context.RequestDetail.Remove(delitem);
                context.SaveChanges();
                requestcontext.Request.Remove(delequest);
                requestcontext.SaveChanges();
                return true;
            }
           
            return true;
        }



    }
}

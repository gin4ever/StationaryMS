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

        public RequestDetailServices(StationeryContext requestcontext, StationeryContext context)
        {
            this.context = context;
            this.requestcontext = requestcontext;
        }

        public List<RequestDetail> GetRequestDetails(int reqId)
        {
            List<RequestDetail> listRequestDetail = context.RequestDetail.Where(i => i.Request_Id.Equals(reqId)).ToList();
            return listRequestDetail;
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

        public void UpdateRequestDetail(RequestDetail request)
        {
            var editrequest = context.RequestDetail.SingleOrDefault(i => i.ItemCode.Equals(request.ItemCode));
            var udpdaterequest = requestcontext.Request.SingleOrDefault(m => m.Request_Id.Equals(editrequest.Request_Id));
            if (editrequest != null)
            {
                editrequest.Quantity = request.Quantity;
                context.SaveChanges();
                requestcontext.SaveChanges();
            }
            else
            {
                //
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

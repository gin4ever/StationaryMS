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

        private StationeryContext context;
        public RequestDetailServices(StationeryContext context)
        {
            this.context = context;
        }

        public List<RequestDetail> GetRequestDetails(int Id)
        {
            List<RequestDetail> listRequestDetail = context.RequestDetail.Where(i => i.Id.Equals(Id)).ToList();
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
    }
}

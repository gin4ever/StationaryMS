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
    }
}

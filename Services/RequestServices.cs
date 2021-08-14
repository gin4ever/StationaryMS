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
        public RequestServices(StationeryContext context)
        {
            this.context = context;
        }
        public List<Request> GetRequests()
        {
            return context.Request.ToList();
        }
    }
}

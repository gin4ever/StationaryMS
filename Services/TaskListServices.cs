using eProject.Models;
using eProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProject.Services
{
    public class TaskListServices : ITaskListServices
    {
        private StationeryContext context;
        public TaskListServices(StationeryContext context)
        {
            this.context = context;
        }

        public Request GetRequest(int requestID)
        {
            var model = context.Request.SingleOrDefault(r => r.Request_Id.Equals(requestID));
            if (model != null)
            {
                return model;
            }
            else
            {
                return null;
            }
        }

        public List<Request> GetRequests()
        {
            List<Request> requests = context.Request.ToList();
            return requests;
        }
    }
}

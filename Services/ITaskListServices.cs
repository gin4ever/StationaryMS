using eProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProject.Services
{
    public interface ITaskListServices
    {
        List<Request> GetRequests();
        Request GetRequest(int requestID);
    }
}

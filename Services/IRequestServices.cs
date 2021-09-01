using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Models;
namespace eProject.Services
{
    public interface IRequestServices
    {
        List<Request> GetRequests();

        Request GetRequest(int id);
   
        bool ApproveRequest(Request request);
        int SaveRequest(Request request);
        int CountRequest(int user_id);
        List<Request> GetRequestsByUserId(int user_id);

        bool DeleteRequest(int rqId);
        bool UpdateRequest(Request updateRequest);
        List<Request> GetRequestsByApproverID(int user_id);
        List<Report> GetRequestsByDepartment(int department_Id);
    }
}

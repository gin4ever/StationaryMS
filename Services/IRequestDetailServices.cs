using eProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProject.Services
{
    public interface IRequestDetailServices
    {
        List<RequestDetail>  GetRequestDetails(int Id);
        RequestDetail GetItem(int rqId);
        bool SaveRequestDetail(RequestDetail requestDetail);
    }
}

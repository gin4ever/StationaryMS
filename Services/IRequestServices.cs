﻿using System;
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
        bool UpdateRequest(Request request);
      //  int SaveRequest(Request request);
        int SaveRequest(Request request);
        int CountRequest(int user_id);
        List<Request> GetRequestsByUserId(int user_id);
    }
}

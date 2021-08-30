﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Models;
namespace eProject.Services
{
    public interface IRequestItemServices
    {
        List<vRequestItem> GetRequestItems();
        vRequestItem GetRequestItem(int id);
        List<Request> GetRequestbyApprover(int approverID);
        List<Request> GetReportByUserId(int user_id);
    }
}

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
    }
}

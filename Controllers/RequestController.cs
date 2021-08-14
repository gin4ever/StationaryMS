using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Services;
namespace eProject.Controllers
{
    public class RequestController : Controller
    {
        private IRequestServices services;
        public RequestController(IRequestServices services)
        {
            this.services = services;
        }
        public IActionResult AdminIndexRequest()
        {
            var model = services.GetRequests();
            return View(model);
        }
    }
}

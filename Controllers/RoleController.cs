using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Services;
namespace eProject.Controllers
{
    public class RoleController : Controller
    {
        private IRoleServices services;
        public RoleController(IRoleServices services)
        {
            this.services = services;
        }
        public IActionResult AdminIndexRole()
        {
            var model = services.GetRoles();
            return View(model);
        }
    }
}

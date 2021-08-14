using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Services;
using eProject.Models;
namespace eProject.Controllers
{
    public class DepartmentController : Controller
    {
        private IDepartmentServices services;
        public DepartmentController(IDepartmentServices services)
        {
            this.services = services;
        }
        public IActionResult AdminIndexDepartment()
        {
            var model = services.GetDepartments();
            return View(model);
        }
    }
}

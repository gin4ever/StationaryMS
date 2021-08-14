using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Services;
using eProject.Models;
namespace eProject.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryServices services;
        public CategoryController(ICategoryServices services)
        {
            this.services = services;
        }
        public IActionResult AdminIndexCategory()
        {
            var model = services.GetCategories();
            return View(model);
        }
    }
}

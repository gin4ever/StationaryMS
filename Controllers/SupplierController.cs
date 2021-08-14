using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Services;
namespace eProject.Controllers
{
    public class SupplierController : Controller
    {
        private ISupplierServices services;
        public SupplierController(ISupplierServices services)
        {
            this.services = services;
        }
        public IActionResult AdminIndexSupplier()
        {
            var model = services.GetSuppliers();
            return View(model);
        }
    }
}

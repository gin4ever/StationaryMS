using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Models;
using eProject.Services;
using X.PagedList;

namespace eProject.Controllers
{
    public class ItemController : Controller
    {
        private IItemServices services;
        public ItemController(IItemServices services)
        {
            this.services = services;
        }
        public IActionResult Index(int? page, string itemName)
        {
            int pageSize = 4;
            int pageNumber = page ?? 1;
            if (string.IsNullOrEmpty(itemName))
            {
                var itemList = services.GetItems().ToPagedList(pageNumber, pageSize);
                ViewBag.data = itemList;
            }
            else
            {
                var itemList = services.GetItems().Where
                    (a => a.Description.Contains(itemName)).OrderByDescending(a => a.Price).ToList().ToPagedList(pageNumber, pageSize);
                ViewBag.data = itemList;
            }
            return View();
        }

        public IActionResult AdminIndexItem(int? page, string itemName)
        {
            int pageSize = 4;
            int pageNumber = page ?? 1;
            if (string.IsNullOrEmpty(itemName))
            {
                var itemList = services.GetItems().ToPagedList(pageNumber, pageSize);
                ViewBag.data = itemList;
            }
            else
            {
                var itemList = services.GetItems().Where
                    (a => a.Description.Contains(itemName)).OrderByDescending(a => a.Price).ToList().ToPagedList(pageNumber, pageSize);
                ViewBag.data = itemList;
            }
            return View();
        }
    }
}

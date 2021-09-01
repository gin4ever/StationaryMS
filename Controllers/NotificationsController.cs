using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eProject.Services;
using eProject.Models;
namespace eProject.Controllers
{
    public class NotificationsController : Controller
    {
        INotiServices _notiService = null;
        List<Noti> _oNotification = new List<Noti>();

        public NotificationsController(INotiServices notiService)
        {
            _notiService = notiService;
        }
        public IActionResult AllNotifications()
        {
            return View();
        }

        public JsonResult GetNotifications(bool bIsGetOnlyUnread = false)
        {
            _oNotification = new List<Noti>();
            _oNotification = _notiService.GetNotifications(bIsGetOnlyUnread);
            return Json(_oNotification);
        }
    }
}

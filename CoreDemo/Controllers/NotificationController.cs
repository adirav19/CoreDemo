using BussinessLayer.Concrete;
using DataAccesLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class NotificationController : Controller
    {
        NotificationManager nm = new NotificationManager(new EFNotificationRepository());

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AllNotification()
        {
            var values = nm.GetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult AddNotification()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddNotification(Notification p)
        {

            p.NotificationTypeSybmol = "mdi mdi-link-variant";
            p.NotificationStatus = true;
            p.NotificationColor = "preview-icon bg-info";
            p.NotificationDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            nm.TAdd(p);
            return RedirectToAction("AllNotification", "Notification");
        }
        public IActionResult ChangeStatus(int id)
        {
            var notificationvalue = nm.TGetById(id);
            
            
            return RedirectToAction("AllNotification", "Notification");
        }
    }
}

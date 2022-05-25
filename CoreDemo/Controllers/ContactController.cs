using BussinessLayer.Concrete;
using CoreDemo.Models.ContactModel;
using DataAccesLayer.Concrete;
using DataAccesLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class ContactController : Controller
    {
        ContactManager cm = new ContactManager(new EFContactRepository());
        Context c = new Context();
        
       
        public IActionResult Index()
        {
            return View();
        }

        public static List<ContactClass> contacts = new List<ContactClass>
        {
            new ContactClass
            {

            }
        };
        [HttpPost]
        public IActionResult sendMessage(ContactClass p)
        {
            Contact contact = new Contact();
            var jsoncontact = JsonConvert.SerializeObject(p);
            contact.ContactDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            contact.ContactStatus = true;
            contact.ContactUserName = p.SenderName;
            contact.ContactSubject = p.MessageTitle;
            contact.ContactMessage = p.MessageContent;
            contact.ContactMail = p.SenderMail;
            cm.ContactAdd(contact);
            return Json(jsoncontact);
        }
        //[HttpPost]
        //public IActionResult Index(Contact p)
        //{
        //    p.ContactDate = DateTime.Parse(DateTime.Now.ToShortDateString());
        //    p.ContactStatus = true;
        //    cm.ContactAdd(p);
        //    return RedirectToAction("Index","Blog");
        //}
    }
}

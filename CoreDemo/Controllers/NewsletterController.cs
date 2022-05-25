using BussinessLayer.Concrete;
using CoreDemo.Models.NewsletterModel;
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
    public class NewsletterController : Controller
    {
        NewsletterManager nm = new NewsletterManager(new EFNewsletteRepository());
        Context c = new Context();
        [HttpGet]
        public PartialViewResult SubscribeMail()
        {
            return PartialView();
        }
        //[HttpPost]
        //public PartialViewResult SubscribeMail(NewsLetter p)
        //{
        //    p.MailStatus = true;
        //    nm.AddNewsletter(p);
        //    return PartialView();
        //}
        public static List<NewsletterClass> newsletter = new List<NewsletterClass>
        {
            new NewsletterClass
            {

            }
        };
        [HttpPost]
        public IActionResult newslettersub(NewsletterClass p)
        {
            NewsLetter n = new NewsLetter();
            var jsonnewsletter = JsonConvert.SerializeObject(p);
            n.MailStatus = true;
            n.Mail = p.Mail;
            nm.AddNewsletter(n);
            
            return Json(jsonnewsletter);
        }
        
    }
}

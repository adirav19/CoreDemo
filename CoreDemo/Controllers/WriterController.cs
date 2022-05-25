using BussinessLayer.Concrete;
using BussinessLayer.ValidationRules;
using CoreDemo.Models;
using DataAccesLayer.Concrete;
using DataAccesLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    [Authorize]
     //[AllowAnonymous]
    public class WriterController : Controller
    {
        WriterManager wm = new WriterManager(new EFWriterRepository());
        public IActionResult Index()
        {
            var usermail = User.Identity.Name;
            ViewBag.v = usermail;
            Context c = new Context();
            var writername = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterName).FirstOrDefault();
            ViewBag.v2 = writername;
            return View();

        }
        public IActionResult WriterProfile()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }

        public PartialViewResult WriterNavbarPartial()
        {
            return PartialView();
        }

        public PartialViewResult WriterFooterPartial()
        {
            return PartialView();
        }
        [HttpGet]
        public IActionResult WriterEditProfile()
        {
            Context c = new Context();
            var usermail = User.Identity.Name;
            var writerID = c.Writers.Where(x => x.WriterMail == usermail)
                .Select(y => y.WriterID)
                .FirstOrDefault();
            var values = wm.GetWriterByID(writerID);
            var writervalues = wm.TGetById(writerID);
            return View(writervalues);
        }
        [HttpPost]
        public IActionResult WriterEditProfile(Writer p)
        {

            WriterValidator wl = new WriterValidator();
            ValidationResult results = wl.Validate(p);
            if (results.IsValid)
            {
                wm.TUpdate(p);
                return RedirectToAction("Index", "Dashboard");

            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View();
            }
        }
        [HttpGet]
        public IActionResult WriterAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult WriterAdd(AddProfileImage p)
        {
            Writer w = new Writer();
            if (p.WriterImage != null)
            {
                var extension = Path.GetExtension(p.WriterImage.FileName);
                var newimage = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFiles/", newimage);
                var stream = new FileStream(location, FileMode.Create);
                p.WriterImage.CopyTo(stream);
                w.WriterImage = newimage;

            }
            w.WriterMail = p.WriterMail;
            w.WriterName = p.WriterName;
            w.WriterPass = p.WriterPass;
            w.WriterStatus = p.WriterStatus;
            w.WriterAbout = p.WriterAbout;

            wm.TAdd(w);

            return View("Index", "Dashboard");
        }
    }
}

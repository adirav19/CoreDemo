using BussinessLayer.Concrete;
using BussinessLayer.ValidationRules;
using DataAccesLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        WriterManager wm = new WriterManager(new EFWriterRepository());
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Writer p)
        {
            WriterValidator validationRules = new WriterValidator();
            ValidationResult result = validationRules.Validate(p);
            if (result.IsValid)
            {
                p.WriterImage = "deneme";
                p.WriterStatus = true;
                p.WriterAbout = "deneme";
                wm.TAdd(p);
                return RedirectToAction("Index", "Blog");           //index sayfasına yönlendir index sayfasıda blogcontollerin içinde

            }
            else
            {
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

                }
            }
            return View();

        }
    }
}

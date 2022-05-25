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
using X.PagedList;//sayfalama için gerekli

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]//yeni alanda çalıştığımız için area admin olarak göstermek gerekli
    [AllowAnonymous]
    public class CategoryController : Controller
    {
        CategoryManager cm = new CategoryManager(new EFCategoryRepository());
        public IActionResult Index(int page = 1)
        {
            var values = cm.GetList().ToPagedList(page, 3);//1. parametre sayfa 2. parametre saydafa kaç tane değer olsun
            return View(values);
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category p)
        {
            CategoryValidator cv = new CategoryValidator();
            ValidationResult result = cv.Validate(p);
            if (result.IsValid)
            {

                
                p.CategoryStatus = true;
                cm.TAdd(p);
                return RedirectToAction("Index", "Category");           //index sayfasına yönlendir index sayfasıda blogcontollerin içinde

            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

                }
            }
            return View();
        }
        public IActionResult CategoryDelete(int id)
        {
            var value = cm.TGetById(id);
            cm.TDelete(value);
            return RedirectToAction("Index");
        }
    }
}

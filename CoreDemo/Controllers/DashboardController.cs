using DataAccesLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    //[Authorize]
    [AllowAnonymous]
    public class DashboardController : Controller
    {
        
        public IActionResult Index()
        {   //solid ezilmiş oldu burda
            Context c = new Context();

            var usermail = User.Identity.Name;
            var writerID = c.Writers.Where(x => x.WriterMail == usermail)
                .Select(y => y.WriterID)
                .FirstOrDefault();
            ViewBag.v1 = c.Blogs.Count().ToString();
            ViewBag.v2 = c.Blogs.Where(x => x.WriterID == writerID).Count();
            ViewBag.v3 = c.Categories.Count();
            return View();
        }
    }
}

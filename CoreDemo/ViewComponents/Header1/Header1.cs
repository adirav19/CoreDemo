using BussinessLayer.Concrete;
using DataAccesLayer.Concrete;
using DataAccesLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.ViewComponents.Header1
{
    public class Header1 : ViewComponent
    {
        WriterManager wm = new WriterManager(new EFWriterRepository());
        Context c = new Context();
        public IViewComponentResult Invoke()
        {

            var usermail = User.Identity.Name;
            var writerID = c.Writers.Where(x => x.WriterMail == usermail)
                .Select(y => y.WriterID)
                .FirstOrDefault();
            var values = wm.GetWriterByID(writerID);
            return View(values);
        }
        //var usermail = User.Identity.Name;
        //var writerID = c.Writers.Where(x => x.WriterMail == usermail)
        //    .Select(y => y.WriterID)
        //    .FirstOrDefault();
    }
}

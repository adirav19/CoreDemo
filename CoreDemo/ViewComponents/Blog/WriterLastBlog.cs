using BussinessLayer.Concrete;
using DataAccesLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.ViewComponents.Blog
{
    public class WriterLastBlog: ViewComponent
    {
        BlogManager bm = new BlogManager(new EFBlogRepository());
        public IViewComponentResult Invoke()
        {
            var values = bm.GetBlogListWithWriter(1);
            return View(values);
        }
    }
}

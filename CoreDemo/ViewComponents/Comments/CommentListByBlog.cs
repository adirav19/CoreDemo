using BussinessLayer.Concrete;
using DataAccesLayer.Concrete;
using DataAccesLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.ViewComponents.Comments
{
    public class CommentListByBlog : ViewComponent
    {
        CommentManager cm = new CommentManager(new EFCommentRepository());
        
        public IViewComponentResult Invoke(int id)
        {

            var values = cm.GetList(id);
            return View(values);
        }

    }
}

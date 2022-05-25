using BussinessLayer.Concrete;
using DataAccesLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.ViewComponents.Comments
{
    public class addComment: ViewComponent
    {
        CommentManager cm = new CommentManager(new EFCommentRepository());

        public IViewComponentResult Invoke(int id,Comment p)
        {
            
            int blogid = id;
            return View();//şuan için bu vievcomponent kullanılmıyor.
        }
    }
}

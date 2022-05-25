using BussinessLayer.Concrete;
using DataAccesLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class Message2Controller : Controller
    { 
        
        Message2Manager mm = new Message2Manager(new EFMessage2Repository());
        public IActionResult InBox()
        {
            int id = 2;
            var values = mm.GetInboxListByWriter(id);
            return View(values);
        }
        [HttpGet]
        public IActionResult MessageDetails(int id)
        {
           
            var values = mm.TGetById(id);

            return View(values);
        }
    }
}

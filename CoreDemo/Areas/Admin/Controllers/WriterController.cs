using BussinessLayer.Concrete;
using CoreDemo.Areas.Admin.Models;
using DataAccesLayer.Abstract;
using DataAccesLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class WriterController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult WriterList()
        {
            var jsonWriters = JsonConvert.SerializeObject(writers);
            return Json(jsonWriters);
        }
        public IActionResult GetWriterByID(int writerid)
        {
            var findwriter = writers.FirstOrDefault(x => x.ID == writerid);
            var josnWriters = JsonConvert.SerializeObject(findwriter);
            return Json(josnWriters);
            
        }

        [HttpPost]
        public IActionResult AddWriter(WriterClass w)
        {
            writers.Add(w);
            var jsonwriters = JsonConvert.SerializeObject(w);
            
            return Json(jsonwriters);

        }

        public IActionResult DeleteWriter(int id)
        {
            var writer = writers.FirstOrDefault(x => x.ID == id);
            writers.Remove(writer);
            return Json(writer);
        }
        public IActionResult UpdateWriter(WriterClass w)
        {
            var writer = writers.FirstOrDefault(x => x.ID == w.ID);
            writer.Name = w.Name;
            var jsonwriter = JsonConvert.SerializeObject(w);
            return Json(jsonwriter);
            
        }
        public static List<WriterClass> writers = new List<WriterClass>
        {
            new WriterClass
            {
                ID = 1,
                Name = "ayşe"

            },
            new WriterClass
            {
                ID = 2,
                Name = "Ahmet"
            },
            new WriterClass
            {
                ID = 3,
                Name = "Buse"
            }
        };

       
    }
}

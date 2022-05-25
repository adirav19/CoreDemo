using ClosedXML.Excel;
using CoreDemo.Areas.Admin.Models;
using DataAccesLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]//yeni alanda çalıştığımız için area admin olarak göstermek gerekli
    [AllowAnonymous]
    public class BlogController : Controller
    {
        
        public IActionResult ExportDynamicExcelBlogList()
        {

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("BlogListesi");
                worksheet.Cell(1, 1).Value = "Blog ID";
                worksheet.Cell(1, 2).Value = "Blog Adı";

                int blogrowCount = 2;
                foreach (var item in BlogTitleList())
                {
                    worksheet.Cell(blogrowCount, 1).Value = item.ID;
                    worksheet.Cell(blogrowCount, 2).Value = item.BlogName;
                    blogrowCount++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "calisma1.xlsx");
                }
            }
        }
        public List<BlogModel2> BlogTitleList()
        {
            List<BlogModel2> bm = new List<BlogModel2>();
            using (var c = new Context())
            {
                bm = c.Blogs.Select(x => new BlogModel2 {
                    ID = x.BlogID,
                    BlogName = x.BlogTitle
                }).ToList();
            }
            return bm;
        }
        public IActionResult BlogTitleListExcel()
        {
            return View();
        }
    }
}

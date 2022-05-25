using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Models
{
    public class addBlogImage
    {

        public int BlogID { get; set; }
        public string BlogTitle { get; set; }
        public string BlogContent { get; set; }
        public IFormFile BlogImage { get; set; }
        public bool BlogStatus { get; set; }
        public string BlogCreateDate { get; set; }
        public string BlogThumbnailImage { get; set; }
        public int CategoryID { get; set; }
        public int WriterID { get; set; }

    }
}

using BussinessLayer.Concrete;
using BussinessLayer.ValidationRules;
using CoreDemo.Models.BlogModel;
using CoreDemo.Models.NewsletterModel;
using DataAccesLayer.Concrete;
using DataAccesLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using IyzipayCore;
using IyzipayCore.Model;
using IyzipayCore.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    //[Authorize]
    [AllowAnonymous]
    public class BlogController : Controller
    {
        CategoryManager cm = new CategoryManager(new EFCategoryRepository());
        Context c = new Context();
        BlogManager bm = new BlogManager(new EFBlogRepository());
        CommentManager ym = new CommentManager(new EFCommentRepository());
        NewsletterManager nm = new NewsletterManager(new EFNewsletteRepository());
        public IActionResult Index()
        {
            var values = bm.GetBlogListWithCategory();
            return View(values);
        }
       
        public static List<CommentClass> comments = new List<CommentClass>
        {
            new CommentClass
            {
            }
        };
        public static List<NewsletterClass> newsletter = new List<NewsletterClass>
        {
            new NewsletterClass
            {

            }
        };
        [HttpPost]
        public IActionResult addComment(CommentClass p)
        {
            Comment k = new Comment();
            k.CommentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            k.CommentStatus = true;
            var jsonwriters = JsonConvert.SerializeObject(p);
            k.BlogID = p.BlogID;
            k.CommentUserName = p.CommentUserName;
            k.CommentTitle = p.CommentTitle;
            k.CommentContent = p.CommentContent;
            ym.CommentAdd(k);
            return Json(jsonwriters);

        }
        [HttpPost]
        public IActionResult newslettersub(NewsletterClass p)
        {
            NewsLetter n = new NewsLetter();
            var jsonnewsletter = JsonConvert.SerializeObject(p);
            n.MailStatus = true;
            n.Mail = p.Mail;
            nm.AddNewsletter(n);
            return Json(jsonnewsletter);
        }
        public IActionResult BlogReadAll(int id)
        {

            ViewBag.i = id;
            var values = bm.GetBlogByID(id);
            return View(values);

        }

        public IActionResult BlogListByWriter()
        {

            var usermail = User.Identity.Name;
            var writerID = c.Writers.Where(x => x.WriterMail == usermail)
                .Select(y => y.WriterID)
                .FirstOrDefault();
            var values = bm.GetListWithCategoryBM(writerID);
            return View(values);
        }
        [HttpGet]
        public IActionResult BlogAdd()
        {
            List<SelectListItem> categoryValue = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();
            ViewBag.cv = categoryValue;
            return View();
        }
        [HttpPost]
        public IActionResult BlogAdd(Blog p)
        {
            BlogValidator bv = new BlogValidator();
            ValidationResult result = bv.Validate(p);
            if (result.IsValid)
            {

                var usermail = User.Identity.Name;
                var writerID = c.Writers.Where(x => x.WriterMail == usermail)
                    .Select(y => y.WriterID)
                    .FirstOrDefault();
                p.BlogStatus = true;
                p.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.WriterID = writerID;
                bm.TAdd(p);
                return RedirectToAction("BlogListByWriter", "Blog");           //index sayfasına yönlendir index sayfasıda blogcontollerin içinde

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
        public IActionResult DeleteBlog(int id)
        {
            var blogvalue = bm.TGetById(id);
            bm.TDelete(blogvalue);
            return RedirectToAction("BlogListByWriter");
        }
        [HttpGet]
        public IActionResult EditBlog(int id)
        {
            List<SelectListItem> categoryValue = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();
            ViewBag.cv = categoryValue;
            var blogvalue = bm.TGetById(id);

            return View(blogvalue);
        }
        [HttpPost]
        public IActionResult EditBlog(Blog p)
        {

            var usermail = User.Identity.Name;
            var writerID = c.Writers.Where(x => x.WriterMail == usermail)
                .Select(y => y.WriterID)
                .FirstOrDefault();
            p.WriterID = writerID;
            p.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.BlogStatus = true;
            bm.TUpdate(p);
            return RedirectToAction("BlogListByWriter");
        }
        [HttpGet]
        public IActionResult CheckOut()
        {

            return View();
        }
        [HttpPost]
        public IActionResult CheckOut(int c)
        {
            Options options = new Options();
            options.ApiKey = "sandbox-JnaRmqcRl58JA1jsmnXAc9eBi57jqdUA";
            options.SecretKey = "sandbox-xCliPrezkpQuAwltagvDuSCPFHknZPE5";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = "123456789";
            request.Price = "1";
            request.PaidPrice = "1.2";
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = "John Doe";
            paymentCard.CardNumber = "5528790000000008";
            paymentCard.ExpireMonth = "12";
            paymentCard.ExpireYear = "2030";
            paymentCard.Cvc = "123";
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = "John";
            buyer.Surname = "Doe";
            buyer.GsmNumber = "+905350000000";
            buyer.Email = "email@email.com";
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = "BI101";
            firstBasketItem.Name = "Binocular";
            firstBasketItem.Category1 = "Collectibles";
            firstBasketItem.Category2 = "Accessories";
            firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            firstBasketItem.Price = "0.3";
            basketItems.Add(firstBasketItem);

            BasketItem secondBasketItem = new BasketItem();
            secondBasketItem.Id = "BI102";
            secondBasketItem.Name = "Game code";
            secondBasketItem.Category1 = "Game";
            secondBasketItem.Category2 = "Online Game Items";
            secondBasketItem.ItemType = BasketItemType.VIRTUAL.ToString();
            secondBasketItem.Price = "0.5";
            basketItems.Add(secondBasketItem);

            BasketItem thirdBasketItem = new BasketItem();
            thirdBasketItem.Id = "BI103";
            thirdBasketItem.Name = "Usb";
            thirdBasketItem.Category1 = "Electronics";
            thirdBasketItem.Category2 = "Usb / Cable";
            thirdBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            thirdBasketItem.Price = "0.2";
            basketItems.Add(thirdBasketItem);
            request.BasketItems = basketItems;

            Payment payment = Payment.Create(request, options);
             
            if(payment.Status == "success")
            {
                return RedirectToAction("Succes");
            }

            return View(c);
        }

        public IActionResult Succes()
        {
            return View();
        }
    }
}

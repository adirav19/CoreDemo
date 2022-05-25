using BussinessLayer.Abstract;
using DataAccesLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Concrete
{
    public class NewsletterManager : INewsletterService
    {
        INewsletterDal _newsletterdal;

        public NewsletterManager(INewsletterDal newsletterdal)
        {
            _newsletterdal = newsletterdal;
        }

        public void AddNewsletter(NewsLetter newsLetter)
        {
            _newsletterdal.Insert(newsLetter);
        }
    }
}

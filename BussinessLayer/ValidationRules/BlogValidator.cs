using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.ValidationRules
{
    public class BlogValidator:AbstractValidator<Blog>
    {
        public BlogValidator()
        {
            RuleFor(x => x.BlogTitle).NotEmpty().WithMessage("Blog başlığı kısmı boş geçilemez");
            RuleFor(x => x.BlogContent).NotEmpty().WithMessage("Blog içeriği boş geçilemez");
            RuleFor(x => x.BlogImage).NotEmpty().WithMessage("Görsel Boş geçilemez");
            RuleFor(x => x.BlogTitle).MaximumLength(150).WithMessage("Lütfen en fazla 150 karakter girin");
            RuleFor(x => x.BlogTitle).MinimumLength(3).WithMessage("Lütfen en az 5 karakter girin");

        }
    }
}

using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.ValidationRules
{
   public class CategoryValidator:AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("kategori kısmı boş geçilemez");
            RuleFor(x => x.CategoryDescription).NotEmpty().WithMessage("kategori açıklaması kısmı boş geçilemez");
            RuleFor(x => x.CategoryName).MaximumLength(50).WithMessage("kategori adı en fazla 50 karakter olmalı");
            RuleFor(x => x.CategoryName).MinimumLength(2).WithMessage("Kategori adı en az 2 karakter olmalıdır");


        }
    }
}

using BussinessLayer.Abstract;
using DataAccesLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        EFCategoryRepository eFCategory;
        public CategoryManager()
        {
            eFCategory = new EFCategoryRepository();
        }
        public void CategoryAdd(Category category)
        {

            eFCategory.Insert(category);
        }

        public void CategoryDelete(Category category)
        {
            eFCategory.Delete(category);
        }

        public void CategoryUpdate(Category category)
        {
            eFCategory.Update(category);
        }

        public Category GetById(int id)
        {
            return eFCategory.GetByID(id);
        }

        public List<Category> GetList()
        {
            return eFCategory.GetListAll();
        }
    }
}

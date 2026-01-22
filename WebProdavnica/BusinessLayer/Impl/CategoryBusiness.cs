using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DAL.Abstract;
using Entities;

namespace BusinessLayer.Impl
{
    public class CategoryBusiness : ICategoryBusiness
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryBusiness(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public List<Category> GetCategories()
        {
            return this.categoryRepository.GetAll();
        }
    }
}

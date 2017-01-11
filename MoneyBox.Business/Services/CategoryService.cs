using MoneyBox.Business.DTO;
using MoneyBox.DAL;
using MoneyBox.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyBox.Business.ObjectMappers;

namespace MoneyBox.Business.Services
{
    public class CategoryService
    {
        private MoneyBoxDb _db;

        public CategoryService(MoneyBoxDb db)
        {
            _db = db;
        }

        public List<CategoryDTO> GetParentCategories()
        {
            var list = _db.Categories.Where(c => c.CategoryId == null).ToList();

            return CategoryMapper.ToDTOList(list);
        }

        public List<CategoryDTO> GetSubCategories(int parentId)
        {
            var list = _db.Categories.Where(c => c.CategoryId == parentId).ToList();
            return CategoryMapper.ToDTOList(list);
        }

       
    }
}

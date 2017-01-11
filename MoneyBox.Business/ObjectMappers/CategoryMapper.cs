using MoneyBox.Business.DTO;
using MoneyBox.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBox.Business.ObjectMappers
{
    public static class CategoryMapper
    {
        public static CategoryDTO ToDTO(Category category)
        {
            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public static List<CategoryDTO> ToDTOList(List<Category> list)
        {
            return list.Select(ToDTO).ToList();
        }
    }
}

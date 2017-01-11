using MoneyBox.Business.DTO;
using MoneyBox.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBox.Business.ObjectMappers
{
    public static class CompanyMapper
    {
        public static CompanyDTO ToDTO(Company company)
        {
            return new CompanyDTO
            {
                Id = company.Id,
                Address = company.Address,
                CategoryId = company.CategoryId,
                Description = company.Description,
                Name = company.Name,
                ParentCategoryId = company.Category.CategoryId.GetValueOrDefault()
            };
        }

        public static Company ToModel(CompanyDTO companyDTO)
        {
            return new Company
            {
                Id = companyDTO.Id,
                Address = companyDTO.Address,
                CategoryId = companyDTO.CategoryId,
                Description = companyDTO.Description,
                Name = companyDTO.Name
            };
        }

        public static List<CompanyDTO> ToDTOList(List<Company> list)
        {
            return list.Select(ToDTO).ToList();
        }

        public static List<Company> ToModelList(List<CompanyDTO> list)
        {
            return list.Select(ToModel).ToList();
        }
    }
}

using MoneyBox.Business.DTO;
using MoneyBox.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBox.Business.ObjectMappers
{
    public class BranchMapper
    {
        public static BranchDTO ToDTO(Branch branch)
        {
            return new BranchDTO
            {
                Id = branch.Id,
                Address = branch.Address,
                Description = branch.Description,
                Name = branch.Name,
                CompanyId = branch.CompanyId
            };
        }

        public static Branch ToModel(BranchDTO branchDTO)
        {
            return new Branch
            {
                Id = branchDTO.Id,
                Address = branchDTO.Address,
                Description = branchDTO.Description,
                Name = branchDTO.Name,
                CompanyId = branchDTO.CompanyId
            };
        }

        public static List<BranchDTO> ToDTOList(List<Branch> list)
        {
            return list.Select(ToDTO).ToList();
        }

        public static List<Branch> ToModelList(List<BranchDTO> list)
        {
            return list.Select(ToModel).ToList();
        }
    }
}

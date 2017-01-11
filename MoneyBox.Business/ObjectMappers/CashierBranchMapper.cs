using MoneyBox.Business.DTO;
using MoneyBox.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBox.Business.ObjectMappers
{
    public class CashierBranchMapper
    {
        public static CashierBranchDTO ToDTO(CashierBranch cashierBranch)
        {
            return new CashierBranchDTO
            {
                Id = cashierBranch.Id,
                UserId = cashierBranch.UserId,
                BranchId = cashierBranch.BranchId,
                CreationDate = cashierBranch.CreationDate,
                Creator = cashierBranch.Creator
              
            };
        }

        public static CashierBranch ToModel(CashierBranchDTO cashierBranchDTO)
        {
            return new CashierBranch
            {
                Id = cashierBranchDTO.Id,
                UserId = cashierBranchDTO.UserId,
                BranchId = cashierBranchDTO.BranchId,
                CreationDate = cashierBranchDTO.CreationDate,
                Creator = cashierBranchDTO.Creator
            };
        }

        public static List<CashierBranchDTO> ToDTOList(List<CashierBranch> list)
        {
            return list.Select(ToDTO).ToList();
        }

        public static List<CashierBranch> ToModelList(List<CashierBranchDTO> list)
        {
            return list.Select(ToModel).ToList();
        }
    }
}

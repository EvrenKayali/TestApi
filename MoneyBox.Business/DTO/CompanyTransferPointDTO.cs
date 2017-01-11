using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyBox.Domain.Models;

namespace MoneyBox.Business.DTO
{
    public class CompanyTransferPointDTO
    {
        public int BranchId { get;  set; }
        public string CustomerId { get; set; }
        public decimal PurchaseAmount { get; set; }
        public string CashierId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBox.Business.DTO
{
    public class TransactionAmountCalculationDTO
    {
        public decimal Amount { get; set; }
        public int  BranchId { get; set; }

    }
}

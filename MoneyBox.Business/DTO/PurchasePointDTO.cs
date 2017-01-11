using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBox.Business.DTO
{
    public class PurchasePointDTO
    {
        public string UserId { get; set; }
        public int AccountId { get; set; }
        public int CompanyId { get; set; }
        public decimal Amount { get; set; }
    }
}

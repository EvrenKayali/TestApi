using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBox.Business.DTO
{
    public class CompanyRegisterDTO
    {
        public CompanyDTO Company { get; set; }
        public decimal DiscountPercentage { get; set; }
    }
}

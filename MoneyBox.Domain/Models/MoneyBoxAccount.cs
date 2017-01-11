using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBox.Domain.Models
{
    public class MoneyBoxAccount
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }

        public virtual MoneyBoxUser User { get; set; }
        public virtual Company Company { get; set; }

    }
}

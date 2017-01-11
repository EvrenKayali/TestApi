using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBox.Domain.Models
{
    public class MoneyBoxTransaction
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public int? BranchId { get; set; }
        public decimal PurchaseAmount { get; set; }
        public decimal TransferAmount { get; set; }
        public int? FromAccountId { get; set; }
        public int? ToAccountId { get; set; }
        public int? CampaignId { get; set; }
        public bool IsPurchase { get; set; }
        public bool IsReverseTransaction { get; set; }

        public virtual MoneyBoxUser User { get; set; }
        public virtual MoneyBoxAccount FromAccount { get; set; }
        public virtual MoneyBoxAccount ToAccount { get; set; }
        public virtual Campaign Campaign { get; set; }
    }
}

using MoneyBox.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBox.DAL.Mappings
{
    public class CashierBranchConfiguration : EntityTypeConfiguration<CashierBranch>
    {
        public CashierBranchConfiguration()
        {
            ToTable("CashierBranch");

            HasRequired<MoneyBoxUser>(b => b.User)
              .WithMany()
              .HasForeignKey(m => m.UserId)
              .WillCascadeOnDelete(false);

            HasRequired<Branch>(b => b.Branch)
               .WithMany()
               .HasForeignKey(m => m.BranchId)
               .WillCascadeOnDelete(false);
        }
    }
}

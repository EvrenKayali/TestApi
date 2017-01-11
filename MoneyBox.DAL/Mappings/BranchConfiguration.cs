using MoneyBox.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBox.DAL.Mappings
{
    public class BranchConfiguration : EntityTypeConfiguration<Branch>
    {
        public BranchConfiguration()
        {
            ToTable("Branch");

            Property(m => m.Name)
                .HasMaxLength(75)
                .IsRequired();

            HasRequired<Company>(b => b.Company)
                .WithMany()
                .HasForeignKey(m => m.CompanyId)
                .WillCascadeOnDelete(false);

        }
    }
}

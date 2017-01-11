using MoneyBox.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBox.DAL.Mappings
{
    public class CampaignConfiguration : EntityTypeConfiguration<Campaign>
    {
        public CampaignConfiguration()
        {
            ToTable("Campaign");

            Property(m => m.Name)
               .HasMaxLength(75)
               .IsRequired();

            Property(u => u.StartDate)
              .HasColumnType("datetime2");

            Property(u => u.EndDate)
              .HasColumnType("datetime2").IsOptional();

            Property(c => c.DiscountPercentage).IsRequired();

            HasRequired<Company>(b => b.Company)
              .WithMany()
              .HasForeignKey(m => m.CompanyId)
              .WillCascadeOnDelete(false);

        }

    }
}

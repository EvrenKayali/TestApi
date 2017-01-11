using Microsoft.AspNet.Identity.EntityFramework;
using MoneyBox.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBox.DAL.Mappings
{
    public class IdentityUserConfiguration : EntityTypeConfiguration<IdentityUser>
    {
        public IdentityUserConfiguration()
        {
            ToTable("User");
        }
    }

    public class MoneyBoxUserConfiguration : EntityTypeConfiguration<MoneyBoxUser>
    {
        public MoneyBoxUserConfiguration()
        {
            Property(p => p.FullName)
                .HasMaxLength(75)
                .IsRequired();

            Property(u => u.BirthDate)
                .HasColumnType("datetime2");

            Property(p => p.CompanyId)
              .IsOptional();

            HasOptional<Company>(b => b.Company)
              .WithMany()
              .HasForeignKey(e => e.CompanyId)
              .WillCascadeOnDelete(false);
        }
    }
}

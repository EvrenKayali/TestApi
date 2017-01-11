using MoneyBox.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBox.DAL.Mappings
{
    public class MoneyBoxAccountConfiguration : EntityTypeConfiguration<MoneyBoxAccount>
    {
        public MoneyBoxAccountConfiguration()
        {
            ToTable("MoneyBoxAccount");

            HasKey<int>(m => m.Id);

            Property(p => p.Name)
                .HasMaxLength(250)
                .IsRequired();

            HasOptional<MoneyBoxUser>(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId);

            HasOptional<Company>(m => m.Company)
                .WithMany()
                .HasForeignKey(m => m.CompanyId);

        }
    }
}

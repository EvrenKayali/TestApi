using MoneyBox.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBox.DAL.Mappings
{
    public class CompanyConfiguration : EntityTypeConfiguration<Company>
    {
        public CompanyConfiguration()
        {
            ToTable("Company");

            Property(p => p.Address)
                .HasMaxLength(250)
                .IsRequired();

            Property(p => p.Description)
                .HasMaxLength(300)
                .IsRequired();

            Property(p => p.Name)
                .HasMaxLength(75)
                .IsRequired();
        }
    }
}

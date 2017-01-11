using MoneyBox.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBox.DAL.Mappings
{
    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            this.ToTable("Category");

            this.HasKey<int>(m => m.Id);
            this.Property(p => p.Name)
                .HasMaxLength(50);

            this.HasOptional(p => p.ParentCategory)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .WillCascadeOnDelete(false);

            Property(m => m.Name)
                .HasMaxLength(25)
                .IsRequired();
        }
    }
}

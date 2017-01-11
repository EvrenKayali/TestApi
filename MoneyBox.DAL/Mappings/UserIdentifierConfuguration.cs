using MoneyBox.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBox.DAL.Mappings
{
    public class UserIdentifierConfuguration : EntityTypeConfiguration<UserIdentifier>
    {
        public UserIdentifierConfuguration()
        {
            ToTable("UserIdentifier");
            Property(m => m.UserId).IsRequired();
            Property(m => m.CreateTime).IsRequired();
            Property(m => m.Code).IsRequired();
            Property(m => m.Expire).IsRequired();
        }
    }
}

using MoneyBox.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBox.DAL.Mappings
{
    public class MoneyBoxTransactionConfuguration : EntityTypeConfiguration<MoneyBoxTransaction>
    {
        public MoneyBoxTransactionConfuguration()
        {
            ToTable("MoneyBoxTransaction");
        }
    }
}

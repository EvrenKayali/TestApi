using Microsoft.AspNet.Identity.EntityFramework;
using MoneyBox.DAL.Mappings;
using MoneyBox.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBox.DAL
{
    public class MoneyBoxDb : IdentityDbContext<MoneyBoxUser>
    {
        public MoneyBoxDb()
            : base("MoneyBox", throwIfV1Schema: false)
        {
          
        }

        public static MoneyBoxDb Create()
        {
            return new MoneyBoxDb();
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MoneyBoxAccount> MoneyBoxAccounts { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<CashierBranch> CashierBranches { get; set; }
        public DbSet<MoneyBoxTransaction> MoneyBoxTransactions { get; set; }
        public DbSet<UserIdentifier> UserIdentifiers { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BranchConfiguration());
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new CompanyConfiguration());
            modelBuilder.Configurations.Add(new MoneyBoxUserConfiguration());
            modelBuilder.Configurations.Add(new MoneyBoxAccountConfiguration());
            modelBuilder.Configurations.Add(new CampaignConfiguration());
            modelBuilder.Configurations.Add(new CashierBranchConfiguration());
            modelBuilder.Configurations.Add(new UserIdentifierConfuguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}

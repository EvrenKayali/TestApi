namespace MoneyBox.DAL.Migrations
{
    using Domain.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MoneyBox.DAL.MoneyBoxDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MoneyBoxDb context)
        {
            string[] roles = new string[] { "Admin", "User", "CompanyOwner", "Employee" };

            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);


            foreach (string role in roles)
            {
                if (!context.Roles.Any(r => r.Name == role))
                {
                    manager.Create(new IdentityRole(role));
                }
            }

            var cat1 = new Category { Name = "Yemek" };
            var cat2 = new Category { Name = "Giyim" };

            var subCat1 = new Category { Name = "Cafe & Restorant", ParentCategory = cat1 };
            var subCat2 = new Category { Name = "Ev Yemekleri", ParentCategory = cat1 };
            var subCat3 = new Category { Name = "Türk Mutfağı", ParentCategory = cat1 };
            var subCat4 = new Category { Name = "Fastfood", ParentCategory = cat1 };

            context.Categories.Add(subCat1);
            context.Categories.Add(subCat2);
            context.Categories.Add(subCat3);
            context.Categories.Add(subCat4);

            var subCat5 = new Category { Name = "İç Giyim", ParentCategory = cat2 };
            var subCat6 = new Category { Name = "Erkek Giyim", ParentCategory = cat2 };
            var subCat7 = new Category { Name = "Kadın Giyim", ParentCategory = cat2 };
            var subCat8 = new Category { Name = "Ayakkabı", ParentCategory = cat2 };

            context.Categories.Add(subCat5);
            context.Categories.Add(subCat6);
            context.Categories.Add(subCat7);
            context.Categories.Add(subCat8);


        }
    }
}

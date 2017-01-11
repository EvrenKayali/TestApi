using MoneyBox.Business.DTO;
using MoneyBox.DAL;
using System;
using MoneyBox.Business.ObjectMappers;
using MoneyBox.Domain.Models;
using System.Threading.Tasks;
using System.Linq;
using MoneyBox.Business.Exceptions;
using Microsoft.AspNet.Identity;
using MoneyBox.Business.IdentityManagers;
using MoneyBox.Business.Helpers;
using System.Collections.Generic;
using System.Data.Entity;

namespace MoneyBox.Business.Services
{
    public class CompanyService
    {
        private MoneyBoxDb _db;

        public CompanyService(MoneyBoxDb db)
        {
            _db = db;
        }


        public async Task<CompanyRegisterDTO> Register(CompanyRegisterDTO company, string userName)
        {
            var user = await _db.Users.FirstOrDefaultAsync(c => c.UserName == userName);
            
            if (user.CompanyId != null)
            {
                throw new BusinessException("This user has already had a company");
            }

            var model = CompanyMapper.ToModel(company.Company);

            user.Company = model;
            model.CreationDate = DateTime.Now;

            _db.Companies.Add(model);

            var campaign = new Campaign
            {
                StartDate = DateTime.Now,
                EndDate = null,
                DiscountPercentage = company.DiscountPercentage,
                Name = company.Company.Name + " varsayılan kampanyası",
                IsActive = true,
                Company = model
            };

            campaign.Company = model;

            _db.Campaigns.Add(campaign);

            var branch = new Branch
            {
                Name = model.Name + " Ana Şube",
                Company = model,
                Description = "Ana Şube"
            };

            _db.Branches.Add(branch);

            var account = new MoneyBoxAccount
            {
                Name = model.Name + "TL Hesabı",
                Company = model
            };

            _db.MoneyBoxAccounts.Add(account);

            return company;
        }

        public async Task<CompanyDTO> GetById(int id)
        {
            var model = await _db.Companies.FirstOrDefaultAsync(c => c.Id == id);

            return CompanyMapper.ToDTO(model);
        }

    }
}
using Microsoft.AspNet.Identity;
using MoneyBox.Business.DTO;
using MoneyBox.Business.Exceptions;
using MoneyBox.Business.Helpers;
using MoneyBox.Business.IdentityManagers;
using MoneyBox.Business.ObjectMappers;
using MoneyBox.DAL;
using MoneyBox.Domain.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyBox.Business.Services
{
    public class AccountService
    {
        private MoneyBoxDb _db;

        public AccountService(MoneyBoxDb db)
        {
            _db = db;
        }

        public async Task<IdentityResult> RegisterUser(RegisterUserDTO user, MoneyBoxUserManager userManager)
        {
            if (await _db.Users.AnyAsync(r => r.PhoneNumber == user.PhoneNumber))
            { 
                throw new BusinessException("Phone number all ready exists.");
            }

            if (!string.IsNullOrEmpty(user.Email) && _db.Users.Any(r => r.Email == user.Email))
            {
                throw new BusinessException("Email all ready exists.");
            }

            var model = new MoneyBoxUser()
            {
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                UserName = UserNameHelper.Transform(user.FullName),
                BirthDate = user.BirthDate,
                Email = user.Email
            };


            IdentityResult result = await userManager.CreateAsync(model, user.Password);

            if (result.Succeeded)
            {
                result = userManager.AddToRole(model.Id, "User");
                user.Id = model.Id;
            }

            var account = new MoneyBoxAccount
            {
                Name =  "TL Hesabı",
                UserId = model.Id
            };

            _db.MoneyBoxAccounts.Add(account);

            
            return result;
        }

        public async Task<IdentityResult> RegisterCompanyOwner(RegisterCompanyAdminDTO user, MoneyBoxUserManager userManager)
        {
            if (_db.Users.Any(r => r.PhoneNumber == user.PhoneNumber))
            {
                throw new BusinessException("Phone number all ready exists.");
            }

            if (_db.Users.Any(r => r.Email == user.Email))
            {
                throw new BusinessException("Email all ready exists.");
            }

            var model = new MoneyBoxUser()
            {
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                UserName = UserNameHelper.Transform(user.FullName),
                Email = user.Email
            };



            IdentityResult result = await userManager.CreateAsync(model, user.Password);

            if (result.Succeeded)
            {
                result = userManager.AddToRole(model.Id, "CompanyOwner");
                user.Id = model.Id;
            }

            return result;
        }

        public async Task<IdentityResult> RegisterCashier(RegisterCompanyAdminDTO user, MoneyBoxUserManager userManager)
        {
            if (_db.Users.Any(r => r.PhoneNumber == user.PhoneNumber))
            {
                throw new BusinessException("Phone number all ready exists.");
            }

            if (_db.Users.Any(r => r.Email == user.Email))
            {
                throw new BusinessException("Email all ready exists.");
            }

            var model = new MoneyBoxUser()
            {
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                UserName = UserNameHelper.Transform(user.FullName),
                Email = user.Email
            };
            

            IdentityResult result = await userManager.CreateAsync(model, user.Password);

            if (result.Succeeded)
            {
                result = userManager.AddToRole(model.Id, "Cashier");
                user.Id = model.Id;
            }

            return result;
        }

        public async Task<UserInfoDTO> GetUserInfo(string userName)
        {
            var model = await _db.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            

            return new UserInfoDTO
            {
                BirthDate = model.BirthDate.GetValueOrDefault(),
                Email = model.Email,
                FullName = model.FullName,
                IsPhoneConfirmed = model.PhoneNumberConfirmed,
                PhoneNumber = model.PhoneNumber,
                Id = model.Id,
                IsEmailConfirmed = model.EmailConfirmed,
                CompanyId = model.CompanyId
            };
        }

        public async Task ConfirmPhoneNumber(PhoneNumberConfirmationDTO model)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.PhoneNumber == model.PhoneNumber && u.Id == model.UserId);

            if(user == null)
                throw new BusinessException("Phone number all ready exists.");

            user.PhoneNumberConfirmed = true;

        }

        public async Task AssignCashierToBranch(CashierBranchDTO cashierBranch )
        {
            var branchList = await _db.CashierBranches.Where(m => m.UserId == cashierBranch.UserId).ToListAsync();
            var branch = _db.Branches.FirstOrDefaultAsync(m => m.Id == cashierBranch.BranchId);
            if (branchList.Count > 0)
            {
                if (branchList.Any(m => m.BranchId == cashierBranch.BranchId))
                {
                    throw new BusinessException("this user has already assigned to branch : " + cashierBranch.BranchId);
                }
                
                if (branchList[0].Branch.CompanyId != branch.Id)
                {
                    throw new BusinessException("this user is working in another company.");
                }
            }

            var model = CashierBranchMapper.ToModel(cashierBranch);
            model.CreationDate = DateTime.Now;
            _db.CashierBranches.Add(model);
        }
    }
}

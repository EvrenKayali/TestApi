using MoneyBox.Business.DTO;
using MoneyBox.Business.Exceptions;
using MoneyBox.Business.ObjectMappers;
using MoneyBox.DAL;
using MoneyBox.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBox.Business.Services
{
    public class UserIdentifierService
    {
        private MoneyBoxDb _db;

        public UserIdentifierService(MoneyBoxDb db)
        {
            _db = db;
        }

        public async Task<int> GenerateUserCode(string userId)
        {
            int code = 0;
            var expireTime = DateTime.Now.AddSeconds(120);
            var identifier = await _db.UserIdentifiers.FirstOrDefaultAsync(m => m.UserId == userId && 
            m.Expire > DateTime.Now);

            if (identifier != null)
            {
                code = identifier.Code;
            }
            else
            {
                var r = new Random();

                var avilableCodes = await _db.UserIdentifiers.Where(i => i.Expire > DateTime.Now)
                    .Select(m => m.Code).ToArrayAsync();

                do
                {
                    code = r.Next(1000, 10000);
                } while (avilableCodes.Contains(code));
                

                var model = new UserIdentifier
                {
                    Id = Guid.NewGuid().ToString(),
                    CreateTime = DateTime.Now,
                    Expire = DateTime.Now.AddSeconds(120),
                    UserId = userId,
                    Code = code
                };

                _db.UserIdentifiers.Add(model);
            }
          

         
            return code;
        }

        public async Task<UserInfoDTO> GetUserByCode(UserIdentifierDTO model)
        {
            var result = await _db.UserIdentifiers.FirstOrDefaultAsync(m => m.Code == model.Code &&
            m.Expire > DateTime.Now);

            if (result == null)
                throw new BusinessException("Invalid user code");

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == result.UserId);
            var userAccount = await _db.MoneyBoxAccounts.FirstOrDefaultAsync(m => m.UserId == user.Id);
            return new UserInfoDTO
            {
                BirthDate = user.BirthDate.GetValueOrDefault(),
                Email = user.Email,
                FullName = user.FullName,
                IsPhoneConfirmed = user.PhoneNumberConfirmed,
                PhoneNumber = user.PhoneNumber,
                Id = user.Id,
                IsEmailConfirmed = user.EmailConfirmed,
                CompanyId = user.CompanyId,
                TotalAmount = userAccount.Amount
            };
        }
    }
}

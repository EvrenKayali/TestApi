using MoneyBox.Business.DTO;
using MoneyBox.Business.Exceptions;
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
    public class TransactionService
    {
        private MoneyBoxDb _db;

        public TransactionService(MoneyBoxDb db)
        {
            _db = db;
        }

        public async Task PurchasePoints(PurchasePointDTO model)
        {
            MoneyBoxAccount account = null; 
            if(model.AccountId <= 0)
            {
                if(model.CompanyId > 0)
                {
                    account = await _db.MoneyBoxAccounts.FirstOrDefaultAsync(a => a.CompanyId == model.CompanyId);
                }
            }
            else
            {
                account = await _db.MoneyBoxAccounts.FirstOrDefaultAsync(a => a.Id == model.AccountId);
            }

            if (account != null)
            {
                var transaction = new MoneyBoxTransaction
                {
                    ToAccount = account,
                    Date = DateTime.Now,
                    PurchaseAmount = 0,
                    TransferAmount = model.Amount,
                    IsPurchase = true,
                    IsReverseTransaction = false,
                    UserId = model.UserId,
                    Id = Guid.NewGuid()
                };
             
                account.Amount += transaction.TransferAmount;
                _db.MoneyBoxTransactions.Add(transaction);
                _db.Entry(account).State = EntityState.Modified;
            }
            else
            {
                throw new BusinessException("Account could not be found.");
            }
           
        }

        public async Task<decimal> CalculateTransactionAmount(TransactionAmountCalculationDTO model)
        {
            
            var branch = await _db.Branches.FirstOrDefaultAsync(b => b.Id == model.BranchId);

            var campaign = await _db.Campaigns.FirstOrDefaultAsync(c => c.CompanyId == branch.CompanyId && 
                            c.IsActive == true && (c.EndDate < DateTime.Now || c.EndDate == null));

            var discount = campaign.DiscountPercentage;

            var total = model.Amount * discount / 100;

            return total;
        }

        public async Task CompanyTransferPoint(CompanyTransferPointDTO model)
        {
            var customerAccount = await _db.MoneyBoxAccounts.FirstOrDefaultAsync(u => u.UserId == model.CustomerId);

            var branch = await _db.Branches.FirstOrDefaultAsync(b => b.Id == model.BranchId);

            var companyAccount = await _db.MoneyBoxAccounts.FirstOrDefaultAsync(a => a.CompanyId == branch.CompanyId);

            //refactor 2 campaign calls.
            var campaign = await _db.Campaigns.FirstOrDefaultAsync(c => c.CompanyId == branch.CompanyId &&
                           c.IsActive == true && (c.EndDate < DateTime.Now || c.EndDate == null));

            var calculationDTO = new TransactionAmountCalculationDTO
            {
                Amount = model.PurchaseAmount,
                BranchId = model.BranchId
            };

            var transferAmount = await CalculateTransactionAmount(calculationDTO);

            var transaction = new MoneyBoxTransaction
            {
                Id = Guid.NewGuid(),
                FromAccount = companyAccount,
                ToAccount = customerAccount,
                Date = DateTime.Now,
                UserId = model.CashierId,
                IsPurchase = false,
                IsReverseTransaction = false,
                BranchId = model.BranchId,
                Campaign = campaign,
                PurchaseAmount = model.PurchaseAmount,
                TransferAmount = transferAmount
            };

            _db.MoneyBoxTransactions.Add(transaction);

            customerAccount.Amount += transferAmount;
            companyAccount.Amount -= transferAmount;
            _db.Entry(customerAccount).State = EntityState.Modified;
            _db.Entry(companyAccount).State = EntityState.Modified;
        }
    }
}

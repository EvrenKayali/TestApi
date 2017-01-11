using MoneyBox.Business.DTO;
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
    public class BranchService
    {
        private MoneyBoxDb _db;

        public BranchService(MoneyBoxDb db)
        {
            _db = db;
        }

        public async Task<List<BranchDTO>>GetByCompanyId(int CompanyId)
        {
            var list = await _db.Branches.Where(b => b.CompanyId == CompanyId).ToListAsync();

            return BranchMapper.ToDTOList(list);
        }

        public BranchDTO SaveBranch(BranchDTO branch)
        {
            var model = BranchMapper.ToModel(branch);

            if(model.Id <= 0)
            {
                model = _db.Branches.Add(model);
            }
            else
            {
                _db.Entry(model).State = EntityState.Modified;
            }

            return BranchMapper.ToDTO(model);
        }
    }
}

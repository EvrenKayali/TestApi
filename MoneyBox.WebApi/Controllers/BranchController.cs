using Microsoft.AspNet.Identity;
using MoneyBox.Business;
using MoneyBox.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace MoneyBox.WebApi.Controllers
{
    [Authorize]
    public class BranchController : ApiController
    {
        // Here I am test branch
        ServiceFactory services;

        public BranchController()
        {
            services = new ServiceFactory();
        }

        [Route("api/companybranches")]
        public async Task<List<BranchDTO>> GetBranches()
        {
            var identity = User.Identity as ClaimsIdentity;
            var companyId = identity.Claims.FirstOrDefault(c => c.Type == "CompanyId").Value;
            
            return await services.BranchService.GetByCompanyId(Convert.ToInt32(companyId));
        }

        [Authorize(Roles = "CompanyOwner")]
        [Route("api/SaveBranch")]
        public BranchDTO SaveBranch(BranchDTO branch)
        {
            var identity = User.Identity as ClaimsIdentity;
            var companyId = identity.Claims.FirstOrDefault(c => c.Type == "CompanyId").Value;

            branch.CompanyId = Convert.ToInt32(companyId);
            services.BranchService.SaveBranch(branch);
            services.Commit();

            return branch;
        }

        [Authorize(Roles ="CompanyOwner")]
        [Route("api/AssignCashier")]
        public async Task AssignCashier (CashierBranchDTO vm)
        {
            vm.Creator = User.Identity.GetUserId();
            await services.AccountService.AssignCashierToBranch(vm);
            await services.CommitAsync();
        }

        protected override void Dispose(bool disposing)
        {
            services.Dispose();
            base.Dispose(disposing);
        }
    }
}

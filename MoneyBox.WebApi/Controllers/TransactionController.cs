using Microsoft.AspNet.Identity;
using MoneyBox.Business;
using MoneyBox.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MoneyBox.WebApi.Controllers
{
    public class TransactionController : ApiController
    {
        ServiceFactory services;

        public TransactionController()
        {
            services = new ServiceFactory();
        }

        [HttpPost]
        [Route("api/CalculatePointAmount")]
        public async Task<decimal> CalculatePoint(TransactionAmountCalculationDTO vm)
        {
            var result = await services.TransactionService.CalculateTransactionAmount(vm);

            return result;
        }

        [HttpPost]
        [Route("api/PurchasePoints")]
        [Authorize(Roles = "CompanyOwner")]
        public async Task PurchasePoints(PurchasePointDTO vm)
        {
            vm.UserId = User.Identity.GetUserId();
            
            await services.TransactionService.PurchasePoints(vm);

            await services.CommitAsync();
        }

        [HttpPost]
        [Route("api/CompanyTransferPoint")]
        [Authorize(Roles = "Cashier")]
        public async Task CompanyTransferPoint(CompanyTransferPointDTO model)
        {
            await services.TransactionService.CompanyTransferPoint(model);
            await services.CommitAsync();
        }

        protected override void Dispose(bool disposing)
        {
            services.Dispose();
            base.Dispose(disposing);
        }
    }
}

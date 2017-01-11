using MoneyBox.Business;
using MoneyBox.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MoneyBox.WebApi.Controllers
{
    public class CompanyRegisterController : ApiController
    {
        ServiceFactory services;

        public CompanyRegisterController()
        {
            services = new ServiceFactory();
        }

        [HttpPost]        
        [Authorize(Roles = "CompanyOwner")]
        public async Task<CompanyRegisterDTO> CreateCompany(CompanyRegisterDTO company)
        {
            var firm = await services.CompanyService.Register(company, User.Identity.Name);
         
            try
            {
                await services.CommitAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return company;
        }


        protected override void Dispose(bool disposing)
        {
            services.Dispose();
            base.Dispose(disposing);
        }
    }
}

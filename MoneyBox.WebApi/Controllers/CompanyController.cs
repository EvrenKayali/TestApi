using MoneyBox.Business;
using MoneyBox.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MoneyBox.WebApi.Controllers
{
    [Authorize]
    public class CompanyController : ApiController
    {
        ServiceFactory services;

        public CompanyController()
        {
            services = new ServiceFactory();
        }

        [Route("api/usercompany")]
        public async Task<CompanyDTO> GetCompany()
        {
            var identity = User.Identity as ClaimsIdentity;
            var companyId = identity.Claims.FirstOrDefault(c => c.Type == "CompanyId").Value;
         
            return await services.CompanyService.GetById(Convert.ToInt32(companyId));
        }

        protected override void Dispose(bool disposing)
        {
            services.Dispose();
            base.Dispose(disposing);
        }
    }
}

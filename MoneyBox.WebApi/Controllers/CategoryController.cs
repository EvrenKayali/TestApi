using MoneyBox.Business;
using MoneyBox.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MoneyBox.WebApi.Controllers
{
    public class CategoryController : ApiController
    {
        ServiceFactory services;

        public CategoryController()
        {
            services = new ServiceFactory();
        }

        [Route("api/categories")]
        public List<CategoryDTO> GetCategories()
        {
            var categories = services.CategoryService.GetParentCategories();
            return categories;
        }

        // GET: api/SubCategories
        [Route("api/SubCategories/{id}")]
        public List<CategoryDTO> GetSubCategories(int id)
        {
            return services.CategoryService.GetSubCategories(id);
        }

        protected override void Dispose(bool disposing)
        {
            services.Dispose();
            base.Dispose(disposing);
        }
    }
}

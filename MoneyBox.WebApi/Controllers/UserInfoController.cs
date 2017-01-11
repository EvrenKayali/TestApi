using Microsoft.AspNet.Identity;
using MoneyBox.Business;
using MoneyBox.Business.DTO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MoneyBox.WebApi.Controllers
{
    [Authorize]
    public class UserInfoController : ApiController
    {
        ServiceFactory services;

        public UserInfoController()
        {
            services = new ServiceFactory();
        }

        [Route("api/ConfirmPhone")]
        public async Task<IHttpActionResult> ConfirmPhoneNumber(PhoneNumberConfirmationDTO model)
        {
            await services.AccountService.ConfirmPhoneNumber(model);
            return Ok();
        }
        [Route("api/GetUserInfo")]
        public async Task<UserInfoDTO> GetUserInfo()
        {
            var userName = HttpContext.Current.User.Identity.Name;
            return await services.AccountService.GetUserInfo(userName);
        }

        [Authorize(Roles = "User")]
        [Route("api/GetUserCode")]
        public async Task<int> GetUserCode()
        {
            var userId = User.Identity.GetUserId();
            var result =  await services.UserIdentifierService.GenerateUserCode(userId);
            await services.CommitAsync();
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Cashier")]
        [Route("api/GetUserByCode")]
        public async Task<UserInfoDTO> GetUserByCode(UserIdentifierDTO model)
        {
            var result = await services.UserIdentifierService.GetUserByCode(model);
            return result;
        }


    }
}

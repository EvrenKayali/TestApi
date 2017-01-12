using MoneyBox.Business.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace MoneyBox.WebApi.Filters
{
    public class BusinessExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is BusinessException)
            {
                var request = context.ActionContext.Request;

                var response = new
                {
                    Error = context.Exception.Message
                };

                context.Response = request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
        }
    }
}
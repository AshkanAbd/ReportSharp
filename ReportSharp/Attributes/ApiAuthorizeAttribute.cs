using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ReportSharp.Services.ApiAuthorizationService;

namespace ReportSharp.Attributes
{
    public class ApiAuthorizeAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var apiAuthorizationService = context.HttpContext.RequestServices.GetService<IApiAuthorizationService>();

            if (apiAuthorizationService != null && !apiAuthorizationService.IsAuthorized()) {
                context.Result = new ForbidResult();
                return;
            }

            await next();
        }
    }
}
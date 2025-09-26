using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookStore.Filters
{
    public class Accessable : ActionFilterAttribute
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var referer = context.HttpContext.Request.Headers["Referer"].ToString();
            if (string.IsNullOrEmpty(referer))
            {
                context.Result = new RedirectResult("/Error/E404");
            }
            return base.OnActionExecutionAsync(context, next);
        }
    }
}

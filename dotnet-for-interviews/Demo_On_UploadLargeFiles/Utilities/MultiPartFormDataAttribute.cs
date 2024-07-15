using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Demo_On_UploadLargeFiles.Utilities;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class MultiPartFormDataAttribute : ActionFilterAttribute
{
    // This method will execute before actual control action method exceutes.
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var request = context.HttpContext.Request;

        if (request.HasFormContentType &&
            request.ContentType.StartsWith("multipart/form-data", StringComparison.OrdinalIgnoreCase))
        {
            // No action required because our expectation is this one.
            return;
        }

        context.Result = new StatusCodeResult(statusCode: StatusCodes.Status415UnsupportedMediaType);
    }
}

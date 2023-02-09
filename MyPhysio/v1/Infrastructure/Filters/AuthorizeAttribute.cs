using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MyPhysio.v1.Infrastructure.Filters
{

    /// <summary>
    /// This class is used to inject the Authorization middleware in pipeline and validate the access
    /// </summary>
    public class AuthorizesAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowanonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowanonymous) return;
            if (!context.HttpContext.User.Claims.Any())
            {
                context.Result = new UnauthorizedObjectResult(
                    new ObjectResult(new
                    {
                        StatusCode = (int)HttpStatusCode.Unauthorized,
                        message = "Access Denied"
                    }));
            }

        }
    }
}

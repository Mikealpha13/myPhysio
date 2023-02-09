using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MyPhysio.v1.Infrastructure.Filters
{
    /// <summary>
    /// This class is created for global exception handler
    /// </summary>
    public class ExceptionFilter:ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionFilter> _logger;
        private readonly IHttpContextAccessor _httpContext;


        public ExceptionFilter(ILogger<ExceptionFilter> logger,IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpContext = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }


        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(_httpContext.HttpContext.TraceIdentifier, context.Exception);

            var error = new
            {
                StatusCodes = (int)HttpStatusCode.InternalServerError,
                Message = "Internal server error .Please contact the TDG Support"
            };

            context.Result = new OkObjectResult(new { StatusCode = HttpStatusCode.InternalServerError, Error = error });
        }
    }
}

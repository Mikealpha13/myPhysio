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
    /// This class is used to configure the validation middleware in Http Pipeline
    /// </summary>
    public class ValidateRequestAttribute:ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);

            if (!context.ModelState.IsValid)
            {
                var entry = context.ModelState.Values.FirstOrDefault();

                var validationmessage = entry.Errors.FirstOrDefault().ErrorMessage;

                context.Result = new OkObjectResult(new
                {
                   StatusCode=(int)HttpStatusCode.BadRequest,
                   message=validationmessage
                });

            }
        }
    }
}

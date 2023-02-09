using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhysio.v1.Extensions.Services
{
    public static class LoggerExtension
    {



        public static void APILogInformation(this ILogger logger,string requestId,string useridentity,string eventname)
        {
            LogContext.PushProperty("Request Identifier", requestId);
            LogContext.PushProperty("User Identifier", useridentity);
            LogContext.PushProperty("Event", eventname);

        }
    }
}

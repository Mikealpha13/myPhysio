using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyPhysio.v1.Infrastructure.HostedServices
{

    /// <summary>
    /// This  is  placeholder for executing hosted services
    /// </summary>
    public class MyPhysioHostedServices : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return null;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return null; 
        }
    }
}

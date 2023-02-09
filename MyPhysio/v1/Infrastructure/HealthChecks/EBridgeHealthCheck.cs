using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyPhysio.v1.Infrastructure.HealthChecks
{

    /// <summary>
    /// This class is used to implement the healthcheck
    /// </summary>
    public class MyPhysioHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() =>
            {
                return HealthCheckResult.Healthy("All Dependecies are healthy", null);
            });
        }
    }
}

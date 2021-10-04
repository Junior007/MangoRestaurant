using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace mango.product.api.Health
{
    public class GeneralCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {

            return Task.FromResult(HealthCheckResult.Healthy(nameof(GeneralCheck)));
        }
    }
}

using mango.product.data.Context;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace mango.product.api.Health
{
    public class ProductDBCheck : IHealthCheck
    {

        private readonly ProductContext _context;
        public ProductDBCheck(ProductContext context)
        {
            _context = context;
        }
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {

            bool isHealthy = _context.IsOpen();

            if (isHealthy)
                return Task.FromResult(HealthCheckResult.Healthy(nameof(ProductDBCheck)));

            return Task.FromResult(HealthCheckResult.Unhealthy(nameof(ProductDBCheck)));
        }
    }
}

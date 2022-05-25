using TariffComparison.Model;

namespace TariffComparison
{
    public interface IProductService
    {
        Task<IEnumerable<ProductCosts>> CalculateCostsAsync(
            int consumption, CancellationToken cancellationToken = default);
    }
}
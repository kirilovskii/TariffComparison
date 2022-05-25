using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TariffComparison.Model;

namespace TariffComparison.Controllers
{
    [ApiController]
    [Route("/v1/[controller]")]
    public class ComparisonController : ControllerBase
    {
        private readonly ILogger<ComparisonController> _logger;
        private readonly IProductService _productService;

        public ComparisonController(ILogger<ComparisonController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductCosts>> GetAsync(
            [Required, Range(0, 1_000_000_000)] int consumption, CancellationToken cancellationToken)
        {
            return (await _productService.CalculateCostsAsync(consumption, cancellationToken))
                .OrderBy(c => c.AnnualCosts);
        }
    }
}
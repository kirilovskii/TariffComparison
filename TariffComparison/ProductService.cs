using Microsoft.EntityFrameworkCore;
using TariffComparison.DbModel;
using TariffComparison.Domain;
using TariffComparison.Model;

namespace TariffComparison
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly ProductContext _context;

        public ProductService(IMapper mapper, ProductContext context)
        {
            ArgumentNullException.ThrowIfNull(mapper);
            ArgumentNullException.ThrowIfNull(context);

            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<ProductCosts>> CalculateCostsAsync(
            int consumption, CancellationToken cancellationToken)
        {
            if (consumption < 0)
            {
                throw new ArgumentException(nameof(consumption));
            }

            List<Product> dbProducts = await _context.Products.ToListAsync(cancellationToken);

            return Map(dbProducts);

            IEnumerable<ProductCosts> Map(IEnumerable<Product> dbProducts)
            {
                foreach (var p in dbProducts)
                {
                    if (_mapper.TryMap(p, out ProductBase? product))
                    {
                        yield return new ProductCosts
                        {
                            TariffName = product!.Name,
                            AnnualCosts = product.GetAnnualCost(consumption)
                        };
                    }
                }
            }
        }
    }
}
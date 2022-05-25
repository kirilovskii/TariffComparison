using Microsoft.EntityFrameworkCore;
using TariffComparison.DbModel;
using TariffComparison.Domain;

namespace TariffComparison.UnitTests
{
    public class ProductServiceTests
    {
        private static readonly Product[] _dbProducts = new[]
        {
            new Product
            {
                ProductId = 1,
                Name = "Basic electricity tariff",
                ProductType = ProductType.Basic,
                UnconditionalCosts = 5m,
                ConsumptionCosts = 0.22m
            },
            new Product
            {
                ProductId = 2,
                Name = "Packaged tariff",
                ProductType = ProductType.Packaged,
                PackageCosts = 800m,
                InclidedInPackage = 4000,
                ConsumptionCosts = 0.3m
            }
        };
        
        [Fact]
        public async Task GivenValidConsumptionValue_WhenInvoked_ThenMapsDataBaseProducts()
        {
            using var productContext = GetProductContext();
            var mapper = Substitute.For<IMapper>();
            var productService = new ProductService(mapper, productContext);

            _ = (await productService.CalculateCostsAsync(0, CancellationToken.None))
                .ToArray();

            mapper.ReceivedWithAnyArgs(_dbProducts.Length).TryMap(Arg.Any<Product>(), out _);
        }

        [Fact]
        public async Task GivenValidConsumptionValue_WhenInvoked_ThenReturnsCaclulationResults()
        {
            using var productContext = GetProductContext();

            var consumption = 42;
            var baseProduct = Substitute.For<ProductBase>("product");
            baseProduct.GetAnnualCost(consumption).Returns(3 * consumption);
            var mapper = Substitute.For<IMapper>();
            mapper.TryMap(Arg.Any<Product>(), out Arg.Any<ProductBase?>())
                .Returns(callInfo =>
                {
                    callInfo[1] = baseProduct;
                    return true;
                });

            var productService = new ProductService(mapper, productContext);

            Model.ProductCosts[] result = (await productService.CalculateCostsAsync(consumption, CancellationToken.None))
                .ToArray();

            result.Should().BeEquivalentTo(
                new[]
                {
                    new Model.ProductCosts
                    {
                        TariffName = "product",
                        AnnualCosts = 42 * 3
                    },
                    new Model.ProductCosts
                    {
                        TariffName = "product",
                        AnnualCosts = 42 * 3
                    }
                });
        }

        private ProductContext GetProductContext()
        {
            var options = new DbContextOptionsBuilder<ProductContext>()
                .UseInMemoryDatabase(databaseName: "ProductsDatabase")
                .Options;

            using (var context = new ProductContext(options))
            if (!context.Products.Any())
            {
                context.Products.AddRange(_dbProducts);
                context.SaveChanges();
            }

            return new ProductContext(options);
        }
    }
}
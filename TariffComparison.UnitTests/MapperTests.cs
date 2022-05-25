using TariffComparison.DbModel;
using TariffComparison.Domain;

namespace TariffComparison.UnitTests
{
    public class MapperTests
    {
        [Theory]
        [InlineData(ProductType.Packaged, true)]
        [InlineData(ProductType.Basic, true)]
        [InlineData((ProductType) 0, false)]
        [InlineData((ProductType) 3, false)]
        public void WhenTryMapIsInvoked_ShouldBeAbleToMapKnownTypes(ProductType productType, bool canMap)
        {
            var mapper = new Mapper();
            var dbProduct = new Product
            {
                Name = "name-1",
                ProductType = productType
            };

            mapper.TryMap(dbProduct, out _)
                .Should().Be(canMap);
        }

        [Theory]
        [InlineData(ProductType.Packaged, typeof(PackagedElectricityTariff))]
        [InlineData(ProductType.Basic, typeof(BasicElectricityTariff))]
        public void WhenTryMapIsInvoked_ShouldMapToApproriateType(ProductType productType, Type resultType)
        {
            var mapper = new Mapper();
            var dbProduct = new Product
            {
                Name = "name-1",
                ProductType = productType
            };

            mapper.TryMap(dbProduct, out ProductBase? product);

            product!.Name.Should().Be(dbProduct.Name);
            product!.GetType().Should().Be(resultType);
        }
    }
}
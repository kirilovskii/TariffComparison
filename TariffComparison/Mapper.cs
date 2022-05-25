using TariffComparison.DbModel;
using TariffComparison.Domain;

namespace TariffComparison
{
    public class Mapper : IMapper
    {
        public bool TryMap(Product productEntity, out ProductBase? productBase)
        {
            ArgumentNullException.ThrowIfNull(productEntity);

            productBase = productEntity.ProductType switch
            {
                ProductType.Basic => new BasicElectricityTariff(
                    productEntity.Name,
                    productEntity.UnconditionalCosts,
                    productEntity.ConsumptionCosts),
                ProductType.Packaged => new PackagedElectricityTariff(
                    productEntity.Name,
                    productEntity.PackageCosts,
                    productEntity.InclidedInPackage,
                    productEntity.ConsumptionCosts),
                _ => default
            };

            return productBase is not null;
        }
    }
}
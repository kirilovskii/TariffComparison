using TariffComparison.DbModel;
using TariffComparison.Domain;

namespace TariffComparison
{
    public interface IMapper
    {
        bool TryMap(Product productEntity, out ProductBase? productBase);
    }
}
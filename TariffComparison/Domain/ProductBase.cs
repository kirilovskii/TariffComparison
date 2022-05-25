namespace TariffComparison.Domain
{
    public abstract class ProductBase
    {
        public ProductBase(string name)
        {
            ArgumentNullException.ThrowIfNull(name);

            Name = name;
        }

        public string Name { get; init; }

        public abstract decimal GetAnnualCost(int consumption);
    }
}
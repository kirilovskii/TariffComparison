namespace TariffComparison.DbModel
{
    public class Product
    {
        public int ProductId { get; init; }

        public string Name { get; init; }

        public ProductType ProductType { get; init; }

        public decimal UnconditionalCosts { get; init; }

        public decimal PackageCosts { get; init; }

        public int InclidedInPackage { get; init; }

        public decimal ConsumptionCosts { get; init; }
    }
}
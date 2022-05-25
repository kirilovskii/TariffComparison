namespace TariffComparison.Domain
{
    public class BasicElectricityTariff : ProductBase
    {
        private readonly decimal _baseCosts;
        private readonly decimal _consumptionCosts;

        public BasicElectricityTariff(
            string name, decimal baseCosts, decimal consumptionCosts) : base(name)
        {
            // validate
            _baseCosts = baseCosts;
            _consumptionCosts = consumptionCosts;
        }

        public override decimal GetAnnualCost(int consumption)
        {
            return 12 * _baseCosts + consumption * _consumptionCosts;
        }
    }
}
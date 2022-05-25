namespace TariffComparison.Domain
{
    public class PackagedElectricityTariff : ProductBase
    {
        private readonly decimal _packageCosts;
        private readonly int _includedPackage;
        private readonly decimal _consumptionCosts;

        public PackagedElectricityTariff(
            string name,
            decimal packageCosts,
            int includedPackage,
            decimal consumptionCosts) : base(name)
        {
            // validate
            _packageCosts = packageCosts;
            _includedPackage = includedPackage;
            _consumptionCosts = consumptionCosts;
        }

        public override decimal GetAnnualCost(int consumption)
        {
            // validate
            int exceedConsumption = consumption - _includedPackage;
            if (exceedConsumption > 0)
            {
                return _packageCosts + exceedConsumption * _consumptionCosts;
            }

            return _packageCosts;
        }
    }
}
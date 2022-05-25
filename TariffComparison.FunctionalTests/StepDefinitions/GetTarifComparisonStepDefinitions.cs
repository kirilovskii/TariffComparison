using System.Net;
using TariffComparison.FunctionalTests.Drivers;

namespace TariffComparison.FunctionalTests.StepDefinitions
{
    [Binding]
    public class GetTarifComparisonStepDefinitions
    {
        private readonly ConparisonContext _conparisonContext;
        private readonly TariffComparisonDriver _driver;

        public GetTarifComparisonStepDefinitions(
            ConparisonContext conparisonContext,
            TariffComparisonDriver driver)
        {
            _conparisonContext = conparisonContext;
            _driver = driver;
        }

        [Given(@"the following products exist:")]
        public void GivenTheFollowingProductsExist(Table table)
        {
            //TODO: Implement dynamic prodict creation
        }

        [Given(@"I am authenticated")]
        public void GivenIAmAuthenticated()
        {
            //TODO: Implement Auth&Auth
        }

        [When(@"I request tarif comparison with consumption '([^']*)'")]
        public async Task WhenIRequestTarifComparisonWithConsumption(int consumption)
        {
            (_conparisonContext.RequestSatusCode, _conparisonContext.ProductCosts)
                = await _driver.GetComparison(consumption);
        }

        [Then(@"the following product comparison is received:")]
        public void ThenTheFollowingProductComparisonIsReceived(IEnumerable<ProductCosts> expectedCosts)
        {
            _conparisonContext.ProductCosts
                .Should()
                .BeEquivalentTo(expectedCosts, options => options.WithStrictOrdering());
        }

        [Then(@"my request fails with the status code '([^']*)'")]
        public void ThenMyRequestFailsWithTheStatusCode(HttpStatusCode expectedStatusCode)
        {
            _conparisonContext.RequestSatusCode
                .Should().Be(expectedStatusCode);
        }
    }
}

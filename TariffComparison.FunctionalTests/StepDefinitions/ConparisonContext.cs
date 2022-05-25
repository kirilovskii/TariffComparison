using System.Net;

namespace TariffComparison.FunctionalTests.StepDefinitions
{
    public class ConparisonContext
    {
        public ProductCosts[]? ProductCosts { get; set; }

        public HttpStatusCode? RequestSatusCode { get; set; }
    }
}

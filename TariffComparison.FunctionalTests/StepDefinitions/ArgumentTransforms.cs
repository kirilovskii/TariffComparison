using System.Net;

namespace TariffComparison.FunctionalTests.StepDefinitions
{
    [Binding]
    public class ArgumentTransforms
    {
        [StepArgumentTransformation]
        HttpStatusCode ConvertToHttpStatusCode(string statusCode)
            => Enum.Parse<HttpStatusCode>(statusCode);


        [StepArgumentTransformation]
        IEnumerable<ProductCosts> ConvertToProductCosts(Table table)
            => table.CreateSet<ProductCosts>();
    }
}

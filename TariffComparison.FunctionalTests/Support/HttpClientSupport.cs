using BoDi;

namespace TariffComparison.FunctionalTests.Support
{
    [Binding]
    public class HttpClientSupport
    {
        //TODO: get from config
        private static readonly Uri BaseEndpoint = new Uri("http://localhost:5141/v1/");

        private readonly IObjectContainer _objectContainer;
        private static readonly HttpClient _client;

        static HttpClientSupport()
        {
            _client = new HttpClient();
            _client.BaseAddress = BaseEndpoint;
        }
        public HttpClientSupport(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void InitializeHttpClient()
        {
            _objectContainer.RegisterInstanceAs(_client);
        }
    }
}

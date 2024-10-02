using WebApp.ApiClient;
using WebApp.Utility;

namespace WebApp.Factory
{
    internal static class ApiClientFactory
    {
        private static Uri apiUri;
        private static string token;

        private static Lazy<DnaApiClient> restClient = new Lazy<DnaApiClient>(
            () => new DnaApiClient(apiUri),
            LazyThreadSafetyMode.ExecutionAndPublication);

        static ApiClientFactory()
        {
            apiUri = new Uri(ApplicationSettings.WebApiUrl);
        }

        public static DnaApiClient Instance
        {
            get
            {
                return restClient.Value;
            }
        }

        public static DnaApiClient InstanceAuthenticated
        {
            get
            {
                return restClient.Value;
            }
        }
    }
}
